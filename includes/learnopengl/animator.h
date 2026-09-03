// LearnOpenGL 中文导读
// 职责：推进动画时钟、递归求节点全局变换，并生成顶点 Shader 使用的最终骨骼矩阵数组。
// 调用链：每帧先 UpdateAnimation，再由调用方把 GetFinalBoneMatrices 的结果写入 finalBonesMatrices uniform。
// 生命周期：m_CurrentAnimation 是非拥有型指针，Animation 必须比 Animator 及其每次更新存活更久。
#pragma once

#include <glm/glm.hpp>
#include <map>
#include <vector>
#include <assimp/scene.h>
#include <assimp/Importer.hpp>
#include <learnopengl/animation.h>
#include <learnopengl/bone.h>

class Animator
{
public:
	Animator(Animation* animation)
	{
		m_CurrentTime = 0.0;
		m_CurrentAnimation = animation;

		// 固定准备 100 个槽位，与示例 Shader 的 MAX_BONES 上限相配；写入前没有额外越界检查。
		m_FinalBoneMatrices.reserve(100);

		for (int i = 0; i < 100; i++)
			m_FinalBoneMatrices.push_back(glm::mat4(1.0f));
	}

	void UpdateAnimation(float dt)
	{
		m_DeltaTime = dt;
		if (m_CurrentAnimation)
		{
			// dt 使用秒；乘 ticks-per-second 后进入 Assimp 关键帧使用的 tick 时间域，并按时长循环。
			m_CurrentTime += m_CurrentAnimation->GetTicksPerSecond() * dt;
			m_CurrentTime = fmod(m_CurrentTime, m_CurrentAnimation->GetDuration());
			CalculateBoneTransform(&m_CurrentAnimation->GetRootNode(), glm::mat4(1.0f));
		}
	}

	void PlayAnimation(Animation* pAnimation)
	{
		// 只切换借用指针并重置时间，不复制或接管 Animation。
		m_CurrentAnimation = pAnimation;
		m_CurrentTime = 0.0f;
	}

	void CalculateBoneTransform(const AssimpNodeData* node, glm::mat4 parentTransform)
	{
		std::string nodeName = node->name;
		glm::mat4 nodeTransform = node->transformation;

		// 有动画通道时以当前 tick 的 TRS 插值替换节点的默认局部变换。
		Bone* Bone = m_CurrentAnimation->FindBone(nodeName);

		if (Bone)
		{
			Bone->Update(m_CurrentTime);
			nodeTransform = Bone->GetLocalTransform();
		}

		// 父全局矩阵左乘局部矩阵，把节点变换累积到模型空间。
		glm::mat4 globalTransformation = parentTransform * nodeTransform;

		auto boneInfoMap = m_CurrentAnimation->GetBoneIDMap();
		if (boneInfoMap.find(nodeName) != boneInfoMap.end())
		{
			int index = boneInfoMap[nodeName].id;
			glm::mat4 offset = boneInfoMap[nodeName].offset;
			// offset 先把绑定姿势顶点带到骨骼局部空间，再由当前层级变换带回动画后的模型空间。
			m_FinalBoneMatrices[index] = globalTransformation * offset;
		}

		for (int i = 0; i < node->childrenCount; i++)
			CalculateBoneTransform(&node->children[i], globalTransformation);
	}

	std::vector<glm::mat4> GetFinalBoneMatrices()
	{
		// 当前接口按值返回，调用方每帧取得的是矩阵数组副本。
		return m_FinalBoneMatrices;
	}

private:
	std::vector<glm::mat4> m_FinalBoneMatrices;
	Animation* m_CurrentAnimation;
	float m_CurrentTime;
	float m_DeltaTime;

};
