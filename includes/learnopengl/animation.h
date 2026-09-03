// LearnOpenGL 中文导读
// 职责：从 Assimp 动画文件复制节点层级和各通道关键帧，并与 Model 的骨骼编号、offset 矩阵建立对应关系。
// 调用链：Model 先导入网格骨骼，Animation 再补齐动画通道，Animator 按时间读取这里保存的 CPU 数据。
// 生命周期：局部 Assimp::Importer 销毁前数据已复制到 STL/GLM 容器；构造参数 Model* 只在构造期间借用，不被保存。
#pragma once

#include <vector>
#include <map>
#include <glm/glm.hpp>
#include <assimp/scene.h>
#include <learnopengl/bone.h>
#include <functional>
#include <learnopengl/animdata.h>
#include <learnopengl/model_animation.h>

struct AssimpNodeData
{
	// Assimp 节点树的自包含副本；Animator 递归它，而不依赖已释放的 aiScene。
	glm::mat4 transformation;
	std::string name;
	int childrenCount;
	std::vector<AssimpNodeData> children;
};

class Animation
{
public:
	Animation() = default;

	Animation(const std::string& animationPath, Model* model)
	{
		// Importer 拥有 scene；本构造函数返回时 scene 会失效，因此后续只保存复制后的层级与关键帧。
		Assimp::Importer importer;
		const aiScene* scene = importer.ReadFile(animationPath, aiProcess_Triangulate);
		assert(scene && scene->mRootNode);
		auto animation = scene->mAnimations[0];
		m_Duration = animation->mDuration;
		m_TicksPerSecond = animation->mTicksPerSecond;
		aiMatrix4x4 globalTransformation = scene->mRootNode->mTransformation;
		globalTransformation = globalTransformation.Inverse();
		// 当前实现没有保存或使用上述逆根变换；最终骨骼矩阵实际由 Animator 的层级全局矩阵乘 offset 得到。
		ReadHierarchyData(m_RootNode, scene->mRootNode);
		ReadMissingBones(animation, *model);
	}

	~Animation()
	{
	}

	Bone* FindBone(const std::string& name)
	{
		// 返回 Animation 内部 vector 元素的非拥有型指针；构造完成后该 vector 不再扩容。
		auto iter = std::find_if(m_Bones.begin(), m_Bones.end(),
			[&](const Bone& Bone)
			{
				return Bone.GetBoneName() == name;
			}
		);
		if (iter == m_Bones.end()) return nullptr;
		else return &(*iter);
	}

	
	inline float GetTicksPerSecond() { return m_TicksPerSecond; }
	inline float GetDuration() { return m_Duration;}
	inline const AssimpNodeData& GetRootNode() { return m_RootNode; }
	inline const std::map<std::string,BoneInfo>& GetBoneIDMap() 
	{ 
		return m_BoneInfoMap;
	}

private:
	void ReadMissingBones(const aiAnimation* animation, Model& model)
	{
		int size = animation->mNumChannels;

		// 与 Model 共享编号表，保证顶点 bone ID、动画通道和最终矩阵数组使用同一索引。
		auto& boneInfoMap = model.GetBoneInfoMap();//getting m_BoneInfoMap from Model class
		int& boneCount = model.GetBoneCount(); //getting the m_BoneCounter from Model class

		//reading channels(bones engaged in an animation and their keyframes)
		for (int i = 0; i < size; i++)
		{
			auto channel = animation->mChannels[i];
			std::string boneName = channel->mNodeName.data;

			if (boneInfoMap.find(boneName) == boneInfoMap.end())
			{
				// 此处只补编号，不写 offset；真正参与网格变形的骨骼应已由 Model 导入阶段提供 offset。
				boneInfoMap[boneName].id = boneCount;
				boneCount++;
			}
			m_Bones.push_back(Bone(channel->mNodeName.data,
				boneInfoMap[channel->mNodeName.data].id, channel));
		}

		m_BoneInfoMap = boneInfoMap;
	}

	void ReadHierarchyData(AssimpNodeData& dest, const aiNode* src)
	{
		assert(src);

		// 递归复制默认局部变换；有动画通道的节点会在 Animator 中被插值结果替换。
		dest.name = src->mName.data;
		dest.transformation = AssimpGLMHelpers::ConvertMatrixToGLMFormat(src->mTransformation);
		dest.childrenCount = src->mNumChildren;

		for (int i = 0; i < src->mNumChildren; i++)
		{
			AssimpNodeData newData;
			ReadHierarchyData(newData, src->mChildren[i]);
			dest.children.push_back(newData);
		}
	}
	float m_Duration;
	int m_TicksPerSecond;
	std::vector<Bone> m_Bones;
	AssimpNodeData m_RootNode;
	std::map<std::string, BoneInfo> m_BoneInfoMap;
};

