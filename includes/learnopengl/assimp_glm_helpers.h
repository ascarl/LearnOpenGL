// LearnOpenGL 中文导读
// 职责：把 Assimp 的矩阵、向量和四元数按 GLM 的构造与索引约定复制为值类型。
// 转换边界：只调整数据表示，不做坐标系翻转、单位缩放或额外的模型/世界空间变换。
// 生命周期：返回值不引用 Assimp 内存，可在 aiScene 与 Importer 释放后继续使用。
#pragma once

#include<assimp/quaternion.h>
#include<assimp/vector3.h>
#include<assimp/matrix4x4.h>
#include<glm/glm.hpp>
#include<glm/gtc/quaternion.hpp>


class AssimpGLMHelpers
{
public:

	static inline glm::mat4 ConvertMatrixToGLMFormat(const aiMatrix4x4& from)
	{
		glm::mat4 to;
		// Assimp 以行成员命名，GLM 用 matrix[column][row] 索引；交换索引写入可保持矩阵各系数的语义位置。
		//the a,b,c,d in assimp is the row ; the 1,2,3,4 is the column
		to[0][0] = from.a1; to[1][0] = from.a2; to[2][0] = from.a3; to[3][0] = from.a4;
		to[0][1] = from.b1; to[1][1] = from.b2; to[2][1] = from.b3; to[3][1] = from.b4;
		to[0][2] = from.c1; to[1][2] = from.c2; to[2][2] = from.c3; to[3][2] = from.c4;
		to[0][3] = from.d1; to[1][3] = from.d2; to[2][3] = from.d3; to[3][3] = from.d4;
		return to;
	}

	static inline glm::vec3 GetGLMVec(const aiVector3D& vec) 
	{ 
		return glm::vec3(vec.x, vec.y, vec.z); 
	}

	static inline glm::quat GetGLMQuat(const aiQuaternion& pOrientation)
	{
		// GLM 四元数构造参数顺序为 w、x、y、z，与成员在内存中的常见展示顺序不同。
		return glm::quat(pOrientation.w, pOrientation.x, pOrientation.y, pOrientation.z);
	}
};