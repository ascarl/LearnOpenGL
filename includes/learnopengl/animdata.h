// LearnOpenGL 中文导读
// 职责：定义网格导入与动画求值共享的骨骼索引和绑定姿势 offset 矩阵。
// 数据契约：id 索引最终骨骼矩阵 uniform；offset 把模型空间顶点变换到该骨骼的绑定姿势局部空间。
// 生命周期：只保存 CPU 值，不拥有 Assimp、Model 或 OpenGL 资源。
#pragma once

#include<glm/glm.hpp>

struct BoneInfo
{
	// Model、Animation 与 Animator 必须对同一骨骼沿用这个编号。
	/*id is index in finalBoneMatrices*/
	int id;

	// 与动画后的节点全局变换相乘，形成最终蒙皮矩阵。
	/*offset matrix transforms vertex from model space to bone space*/
	glm::mat4 offset;

};
#pragma once
