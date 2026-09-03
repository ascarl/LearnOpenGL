// LearnOpenGL 中文导读
// 职责：提供场景层级 Transform、相机视锥平面、包围体测试，以及递归更新/剔除/绘制的教学实现。
// 所有权：Entity 用 unique_ptr 独占 children 和 AABB；parent、pModel 只是反向/外部借用指针，Model 必须比实体树存活更久。
// 调用链：调用方先更新根实体矩阵并由 Camera 建立 Frustum，再激活 Shader 后调用 drawSelfAndChild。
// 包含边界：本头直接使用 Camera、Model、Shader；现有示例会在它之前包含这些类型及 GLM 矩阵变换定义。
#ifndef ENTITY_H
#define ENTITY_H

#include <glm/glm.hpp> //glm::mat4
#include <list> //std::list
#include <array> //std::array
#include <memory> //std::unique_ptr

class Transform
{
protected:
	// m_pos/m_eulerRot/m_scale 位于父节点局部空间；m_modelMatrix 缓存累积后的模型到世界变换。
	//Local space information
	glm::vec3 m_pos = { 0.0f, 0.0f, 0.0f };
	glm::vec3 m_eulerRot = { 0.0f, 0.0f, 0.0f }; //In degrees
	glm::vec3 m_scale = { 1.0f, 1.0f, 1.0f };

	//Global space information concatenate in matrix
	glm::mat4 m_modelMatrix = glm::mat4(1.0f);

	//Dirty flag
	bool m_isDirty = true;

protected:
	glm::mat4 getLocalModelMatrix()
	{
		// 欧拉角按 Y * X * Z 组合，再形成 T * R * S；矩阵右侧的缩放最先作用于顶点。
		const glm::mat4 transformX = glm::rotate(glm::mat4(1.0f), glm::radians(m_eulerRot.x), glm::vec3(1.0f, 0.0f, 0.0f));
		const glm::mat4 transformY = glm::rotate(glm::mat4(1.0f), glm::radians(m_eulerRot.y), glm::vec3(0.0f, 1.0f, 0.0f));
		const glm::mat4 transformZ = glm::rotate(glm::mat4(1.0f), glm::radians(m_eulerRot.z), glm::vec3(0.0f, 0.0f, 1.0f));

		// Y * X * Z
		const glm::mat4 rotationMatrix = transformY * transformX * transformZ;

		// translation * rotation * scale (also know as TRS matrix)
		return glm::translate(glm::mat4(1.0f), m_pos) * rotationMatrix * glm::scale(glm::mat4(1.0f), m_scale);
	}
public:

	void computeModelMatrix()
	{
		m_modelMatrix = getLocalModelMatrix();
		m_isDirty = false;
	}

	void computeModelMatrix(const glm::mat4& parentGlobalModelMatrix)
	{
		// 父世界矩阵左乘本地矩阵，使当前节点及其包围体落到世界空间。
		m_modelMatrix = parentGlobalModelMatrix * getLocalModelMatrix();
		m_isDirty = false;
	}

	void setLocalPosition(const glm::vec3& newPosition)
	{
		m_pos = newPosition;
		m_isDirty = true;
	}

	void setLocalRotation(const glm::vec3& newRotation)
	{
		m_eulerRot = newRotation;
		m_isDirty = true;
	}

	void setLocalScale(const glm::vec3& newScale)
	{
		m_scale = newScale;
		m_isDirty = true;
	}

	const glm::vec3& getGlobalPosition() const
	{
		// 当前签名把 mat4 的 vec4 列转换成 vec3 临时值后以引用返回，返回时引用已悬空，调用者不能安全使用。
		return m_modelMatrix[3];
	}

	const glm::vec3& getLocalPosition() const
	{
		return m_pos;
	}

	const glm::vec3& getLocalRotation() const
	{
		return m_eulerRot;
	}

	const glm::vec3& getLocalScale() const
	{
		return m_scale;
	}

	const glm::mat4& getModelMatrix() const
	{
		return m_modelMatrix;
	}

	glm::vec3 getRight() const
	{
		return m_modelMatrix[0];
	}


	glm::vec3 getUp() const
	{
		return m_modelMatrix[1];
	}

	glm::vec3 getBackward() const
	{
		return m_modelMatrix[2];
	}

	glm::vec3 getForward() const
	{
		return -m_modelMatrix[2];
	}

	glm::vec3 getGlobalScale() const
	{
		// 模型矩阵前三列的长度给出层级缩放后的各局部轴尺度。
		return { glm::length(getRight()), glm::length(getUp()), glm::length(getBackward()) };
	}

	bool isDirty() const
	{
		return m_isDirty;
	}
};

struct Plane
{
	// 平面采用 dot(normal, point) - distance；视锥各法线朝向可见半空间内部。
	glm::vec3 normal = { 0.f, 1.f, 0.f }; // unit vector
	float     distance = 0.f;        // Distance with origin

	Plane() = default;

	Plane(const glm::vec3& p1, const glm::vec3& norm)
		: normal(glm::normalize(norm)),
		distance(glm::dot(normal, p1))
	{}

	float getSignedDistanceToPlane(const glm::vec3& point) const
	{
		return glm::dot(normal, point) - distance;
	}
};

struct Frustum
{
	Plane topFace;
	Plane bottomFace;

	Plane rightFace;
	Plane leftFace;

	Plane farFace;
	Plane nearFace;
};

struct BoundingVolume
{
	// 派生包围体先转换到世界空间再测试六个平面；是否保守取决于具体估计，Sphere 的尺度估计仅在无剪切/变换轴正交时保证保守。
	virtual bool isOnFrustum(const Frustum& camFrustum, const Transform& transform) const = 0;

	virtual bool isOnOrForwardPlane(const Plane& plane) const = 0;

	bool isOnFrustum(const Frustum& camFrustum) const
	{
		return (isOnOrForwardPlane(camFrustum.leftFace) &&
			isOnOrForwardPlane(camFrustum.rightFace) &&
			isOnOrForwardPlane(camFrustum.topFace) &&
			isOnOrForwardPlane(camFrustum.bottomFace) &&
			isOnOrForwardPlane(camFrustum.nearFace) &&
			isOnOrForwardPlane(camFrustum.farFace));
	};
};

struct Sphere : public BoundingVolume
{
	glm::vec3 center{ 0.f, 0.f, 0.f };
	float radius{ 0.f };

	Sphere(const glm::vec3& inCenter, float inRadius)
		: BoundingVolume{}, center{ inCenter }, radius{ inRadius }
	{}

	bool isOnOrForwardPlane(const Plane& plane) const final
	{
		return plane.getSignedDistanceToPlane(center) > -radius;
	}

	bool isOnFrustum(const Frustum& camFrustum, const Transform& transform) const final
	{
		// generateSphereBV 以完整对角线存 radius，此处乘 0.5 还原半径；最大列长度仅在无剪切/轴正交时保证覆盖实际尺度。
		// 父非均匀缩放叠加子旋转可产生 shear，此值可能小于最大奇异值，使球偏小并误剔除；默认 AABB 路径不受此特定问题影响。
		//Get global scale thanks to our transform
		const glm::vec3 globalScale = transform.getGlobalScale();

		//Get our global center with process it with the global model matrix of our transform
		const glm::vec3 globalCenter{ transform.getModelMatrix() * glm::vec4(center, 1.f) };

		//To wrap correctly our shape, we need the maximum scale scalar.
		const float maxScale = std::max(std::max(globalScale.x, globalScale.y), globalScale.z);

		//Max scale is assuming for the diameter. So, we need the half to apply it to our radius
		Sphere globalSphere(globalCenter, radius * (maxScale * 0.5f));

		//Check Firstly the result that have the most chance to failure to avoid to call all functions.
		return (globalSphere.isOnOrForwardPlane(camFrustum.leftFace) &&
			globalSphere.isOnOrForwardPlane(camFrustum.rightFace) &&
			globalSphere.isOnOrForwardPlane(camFrustum.farFace) &&
			globalSphere.isOnOrForwardPlane(camFrustum.nearFace) &&
			globalSphere.isOnOrForwardPlane(camFrustum.topFace) &&
			globalSphere.isOnOrForwardPlane(camFrustum.bottomFace));
	};
};

struct SquareAABB : public BoundingVolume
{
	glm::vec3 center{ 0.f, 0.f, 0.f };
	float extent{ 0.f };

	SquareAABB(const glm::vec3& inCenter, float inExtent)
		: BoundingVolume{}, center{ inCenter }, extent{ inExtent }
	{}

	bool isOnOrForwardPlane(const Plane& plane) const final
	{
		// Compute the projection interval radius of b onto L(t) = b.c + t * p.n
		const float r = extent * (std::abs(plane.normal.x) + std::abs(plane.normal.y) + std::abs(plane.normal.z));
		return -r <= plane.getSignedDistanceToPlane(center);
	}

	bool isOnFrustum(const Frustum& camFrustum, const Transform& transform) const final
	{
		// 将旋转/缩放后的三个局部轴投影到世界 XYZ，构造可包住它们的世界轴对齐立方体。
		//Get global scale thanks to our transform
		const glm::vec3 globalCenter{ transform.getModelMatrix() * glm::vec4(center, 1.f) };

		// Scaled orientation
		const glm::vec3 right = transform.getRight() * extent;
		const glm::vec3 up = transform.getUp() * extent;
		const glm::vec3 forward = transform.getForward() * extent;

		const float newIi = std::abs(glm::dot(glm::vec3{ 1.f, 0.f, 0.f }, right)) +
			std::abs(glm::dot(glm::vec3{ 1.f, 0.f, 0.f }, up)) +
			std::abs(glm::dot(glm::vec3{ 1.f, 0.f, 0.f }, forward));

		const float newIj = std::abs(glm::dot(glm::vec3{ 0.f, 1.f, 0.f }, right)) +
			std::abs(glm::dot(glm::vec3{ 0.f, 1.f, 0.f }, up)) +
			std::abs(glm::dot(glm::vec3{ 0.f, 1.f, 0.f }, forward));

		const float newIk = std::abs(glm::dot(glm::vec3{ 0.f, 0.f, 1.f }, right)) +
			std::abs(glm::dot(glm::vec3{ 0.f, 0.f, 1.f }, up)) +
			std::abs(glm::dot(glm::vec3{ 0.f, 0.f, 1.f }, forward));

		const SquareAABB globalAABB(globalCenter, std::max(std::max(newIi, newIj), newIk));

		return (globalAABB.isOnOrForwardPlane(camFrustum.leftFace) &&
			globalAABB.isOnOrForwardPlane(camFrustum.rightFace) &&
			globalAABB.isOnOrForwardPlane(camFrustum.topFace) &&
			globalAABB.isOnOrForwardPlane(camFrustum.bottomFace) &&
			globalAABB.isOnOrForwardPlane(camFrustum.nearFace) &&
			globalAABB.isOnOrForwardPlane(camFrustum.farFace));
	};
};

struct AABB : public BoundingVolume
{
	glm::vec3 center{ 0.f, 0.f, 0.f };
	glm::vec3 extents{ 0.f, 0.f, 0.f };

	AABB(const glm::vec3& min, const glm::vec3& max)
		: BoundingVolume{}, center{ (max + min) * 0.5f }, extents{ max.x - center.x, max.y - center.y, max.z - center.z }
	{}

	AABB(const glm::vec3& inCenter, float iI, float iJ, float iK)
		: BoundingVolume{}, center{ inCenter }, extents{ iI, iJ, iK }
	{}

	std::array<glm::vec3, 8> getVertice() const
	{
		std::array<glm::vec3, 8> vertice;
		vertice[0] = { center.x - extents.x, center.y - extents.y, center.z - extents.z };
		vertice[1] = { center.x + extents.x, center.y - extents.y, center.z - extents.z };
		vertice[2] = { center.x - extents.x, center.y + extents.y, center.z - extents.z };
		vertice[3] = { center.x + extents.x, center.y + extents.y, center.z - extents.z };
		vertice[4] = { center.x - extents.x, center.y - extents.y, center.z + extents.z };
		vertice[5] = { center.x + extents.x, center.y - extents.y, center.z + extents.z };
		vertice[6] = { center.x - extents.x, center.y + extents.y, center.z + extents.z };
		vertice[7] = { center.x + extents.x, center.y + extents.y, center.z + extents.z };
		return vertice;
	}

	//see https://gdbooks.gitbooks.io/3dcollisions/content/Chapter2/static_aabb_plane.html
	bool isOnOrForwardPlane(const Plane& plane) const final
	{
		// Compute the projection interval radius of b onto L(t) = b.c + t * p.n
		const float r = extents.x * std::abs(plane.normal.x) + extents.y * std::abs(plane.normal.y) +
			extents.z * std::abs(plane.normal.z);

		return -r <= plane.getSignedDistanceToPlane(center);
	}

	bool isOnFrustum(const Frustum& camFrustum, const Transform& transform) const final
	{
		// 把有向且缩放后的局部 AABB 三轴投影到世界轴，得到保守的世界空间 AABB 半尺寸。
		//Get global scale thanks to our transform
		const glm::vec3 globalCenter{ transform.getModelMatrix() * glm::vec4(center, 1.f) };

		// Scaled orientation
		const glm::vec3 right = transform.getRight() * extents.x;
		const glm::vec3 up = transform.getUp() * extents.y;
		const glm::vec3 forward = transform.getForward() * extents.z;

		const float newIi = std::abs(glm::dot(glm::vec3{ 1.f, 0.f, 0.f }, right)) +
			std::abs(glm::dot(glm::vec3{ 1.f, 0.f, 0.f }, up)) +
			std::abs(glm::dot(glm::vec3{ 1.f, 0.f, 0.f }, forward));

		const float newIj = std::abs(glm::dot(glm::vec3{ 0.f, 1.f, 0.f }, right)) +
			std::abs(glm::dot(glm::vec3{ 0.f, 1.f, 0.f }, up)) +
			std::abs(glm::dot(glm::vec3{ 0.f, 1.f, 0.f }, forward));

		const float newIk = std::abs(glm::dot(glm::vec3{ 0.f, 0.f, 1.f }, right)) +
			std::abs(glm::dot(glm::vec3{ 0.f, 0.f, 1.f }, up)) +
			std::abs(glm::dot(glm::vec3{ 0.f, 0.f, 1.f }, forward));

		const AABB globalAABB(globalCenter, newIi, newIj, newIk);

		return (globalAABB.isOnOrForwardPlane(camFrustum.leftFace) &&
			globalAABB.isOnOrForwardPlane(camFrustum.rightFace) &&
			globalAABB.isOnOrForwardPlane(camFrustum.topFace) &&
			globalAABB.isOnOrForwardPlane(camFrustum.bottomFace) &&
			globalAABB.isOnOrForwardPlane(camFrustum.nearFace) &&
			globalAABB.isOnOrForwardPlane(camFrustum.farFace));
	};
};

Frustum createFrustumFromCamera(const Camera& cam, float aspect, float fovY, float zNear, float zFar)
{
	// fovY 必须是弧度；六个平面由相机世界空间基向量构造，法线统一朝向视锥内部。
	Frustum     frustum;
	const float halfVSide = zFar * tanf(fovY * .5f);
	const float halfHSide = halfVSide * aspect;
	const glm::vec3 frontMultFar = zFar * cam.Front;

	frustum.nearFace = { cam.Position + zNear * cam.Front, cam.Front };
	frustum.farFace = { cam.Position + frontMultFar, -cam.Front };
	frustum.rightFace = { cam.Position, glm::cross(frontMultFar - cam.Right * halfHSide, cam.Up) };
	frustum.leftFace = { cam.Position, glm::cross(cam.Up, frontMultFar + cam.Right * halfHSide) };
	frustum.topFace = { cam.Position, glm::cross(cam.Right, frontMultFar - cam.Up * halfVSide) };
	frustum.bottomFace = { cam.Position, glm::cross(frontMultFar + cam.Up * halfVSide, cam.Right) };
	return frustum;
}

AABB generateAABB(const Model& model)
{
	// 遍历 Model 保留的 CPU 顶点，在模型空间求 min/max；不会读取 GPU 缓冲。
	// 现有 max 初值使用 numeric_limits<float>::min()，对坐标全为负值的模型存在既有边界问题。
	glm::vec3 minAABB = glm::vec3(std::numeric_limits<float>::max());
	glm::vec3 maxAABB = glm::vec3(std::numeric_limits<float>::min());
	for (auto&& mesh : model.meshes)
	{
		for (auto&& vertex : mesh.vertices)
		{
			minAABB.x = std::min(minAABB.x, vertex.Position.x);
			minAABB.y = std::min(minAABB.y, vertex.Position.y);
			minAABB.z = std::min(minAABB.z, vertex.Position.z);

			maxAABB.x = std::max(maxAABB.x, vertex.Position.x);
			maxAABB.y = std::max(maxAABB.y, vertex.Position.y);
			maxAABB.z = std::max(maxAABB.z, vertex.Position.z);
		}
	}
	return AABB(minAABB, maxAABB);
}

Sphere generateSphereBV(const Model& model)
{
	// 先求模型空间 AABB，再以中心和对角线长度构造球；Sphere 世界变换处乘 0.5 还原半径。
	// 与 generateAABB 相同，max 以 numeric_limits<float>::min() 初始化会误算全负坐标；空 Model 也没有可生成的有效边界。
	glm::vec3 minAABB = glm::vec3(std::numeric_limits<float>::max());
	glm::vec3 maxAABB = glm::vec3(std::numeric_limits<float>::min());
	for (auto&& mesh : model.meshes)
	{
		for (auto&& vertex : mesh.vertices)
		{
			minAABB.x = std::min(minAABB.x, vertex.Position.x);
			minAABB.y = std::min(minAABB.y, vertex.Position.y);
			minAABB.z = std::min(minAABB.z, vertex.Position.z);

			maxAABB.x = std::max(maxAABB.x, vertex.Position.x);
			maxAABB.y = std::max(maxAABB.y, vertex.Position.y);
			maxAABB.z = std::max(maxAABB.z, vertex.Position.z);
		}
	}

	return Sphere((maxAABB + minAABB) * 0.5f, glm::length(minAABB - maxAABB));
}

class Entity
{
public:
	// children 拥有子节点；parent 仅指回父节点，不参与释放，因此不会形成所有权环。
	//Scene graph
	std::list<std::unique_ptr<Entity>> children;
	Entity* parent = nullptr;

	//Space information
	Transform transform;

	// 模型由场景外部拥有；boundingVolume 则由当前 Entity 独占。
	Model* pModel = nullptr;
	std::unique_ptr<AABB> boundingVolume;


	// constructor, expects a filepath to a 3D model.
	Entity(Model& model) : pModel{ &model }
	{
		// 默认从 Model 的 CPU 顶点生成局部空间 AABB；不会复制 Model 本身。
		boundingVolume = std::make_unique<AABB>(generateAABB(model));
		//boundingVolume = std::make_unique<Sphere>(generateSphereBV(model));
	}

	AABB getGlobalAABB()
	{
		//Get global scale thanks to our transform
		const glm::vec3 globalCenter{ transform.getModelMatrix() * glm::vec4(boundingVolume->center, 1.f) };

		// Scaled orientation
		const glm::vec3 right = transform.getRight() * boundingVolume->extents.x;
		const glm::vec3 up = transform.getUp() * boundingVolume->extents.y;
		const glm::vec3 forward = transform.getForward() * boundingVolume->extents.z;

		const float newIi = std::abs(glm::dot(glm::vec3{ 1.f, 0.f, 0.f }, right)) +
			std::abs(glm::dot(glm::vec3{ 1.f, 0.f, 0.f }, up)) +
			std::abs(glm::dot(glm::vec3{ 1.f, 0.f, 0.f }, forward));

		const float newIj = std::abs(glm::dot(glm::vec3{ 0.f, 1.f, 0.f }, right)) +
			std::abs(glm::dot(glm::vec3{ 0.f, 1.f, 0.f }, up)) +
			std::abs(glm::dot(glm::vec3{ 0.f, 1.f, 0.f }, forward));

		const float newIk = std::abs(glm::dot(glm::vec3{ 0.f, 0.f, 1.f }, right)) +
			std::abs(glm::dot(glm::vec3{ 0.f, 0.f, 1.f }, up)) +
			std::abs(glm::dot(glm::vec3{ 0.f, 0.f, 1.f }, forward));

		return AABB(globalCenter, newIi, newIj, newIk);
	}

	//Add child. Argument input is argument of any constructor that you create. By default you can use the default constructor and don't put argument input.
	template<typename... TArgs>
	void addChild(TArgs&... args)
	{
		// unique_ptr 保存新子节点，并设置仅用于变换回溯的非拥有型 parent 指针。
		children.emplace_back(std::make_unique<Entity>(args...));
		children.back()->parent = this;
	}

	//Update transform if it was changed
	void updateSelfAndChild()
	{
		// 当前节点变脏时强制重算整棵子树；否则仍递归，让独立变脏的后代自行更新。
		if (transform.isDirty()) {
			forceUpdateSelfAndChild();
			return;
		}
			
		for (auto&& child : children)
		{
			child->updateSelfAndChild();
		}
	}

	//Force update of transform even if local space don't change
	void forceUpdateSelfAndChild()
	{
		if (parent)
			transform.computeModelMatrix(parent->transform.getModelMatrix());
		else
			transform.computeModelMatrix();

		for (auto&& child : children)
		{
			child->forceUpdateSelfAndChild();
		}
	}


	void drawSelfAndChild(const Frustum& frustum, Shader& ourShader, unsigned int& display, unsigned int& total)
	{
		// Shader 必须已激活；可见节点写入 model uniform 后交给外部 Model 绘制。
		if (boundingVolume->isOnFrustum(frustum, transform))
		{
			ourShader.setMat4("model", transform.getModelMatrix());
			pModel->Draw(ourShader);
			display++;
		}
		total++;

		// 即使父包围体不可见也继续测试子节点，避免假设父体必然完整包住所有后代。
		for (auto&& child : children)
		{
			child->drawSelfAndChild(frustum, ourShader, display, total);
		}
	}
};
#endif
