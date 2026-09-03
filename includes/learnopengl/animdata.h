// LearnOpenGL 中文导读
// 职责：定义网格导入与动画求值共享的骨骼索引和绑定姿势 offset 矩阵。
// 数据契约：id 索引最终骨骼矩阵 uniform；offset（Assimp mOffsetMatrix）把 mesh space 的绑定姿势顶点变换到 bind-pose bone space。
// 生命周期：只保存 CPU 值，不拥有 Assimp、Model 或 OpenGL 资源。
#pragma once

#include<glm/glm.hpp>

struct BoneInfo
{
	// Model、Animation 与 Animator 必须对同一骨骼沿用这个编号。
	/*id is index in finalBoneMatrices*/
	int id;

	// Animator 将其右乘到当前节点的根层级累计变换；结果位于当前动画根层级坐标，依赖网格与层级空间兼容。
	/*offset matrix transforms vertex from model space to bone space*/
	glm::mat4 offset;

};
#pragma once
