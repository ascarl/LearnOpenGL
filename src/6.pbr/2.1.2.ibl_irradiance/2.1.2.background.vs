#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：环境背景顶点着色器，以世界方向采样 Cubemap 并移除相机平移。
// 输入输出：WorldPos=aPos 作为采样方向，rotView 仅保留 view 旋转。
// 核心算法：clipPos.xyww 令深度除法后固定为 1，使天空盒通过 LEQUAL 深度测试落在最远平面。
layout (location = 0) in vec3 aPos;

uniform mat4 projection;
uniform mat4 view;

out vec3 WorldPos;

void main()
{
    WorldPos = aPos;

	mat4 rotView = mat4(mat3(view));
	vec4 clipPos = projection * rotView * vec4(WorldPos, 1.0);

	gl_Position = clipPos.xyww;
}