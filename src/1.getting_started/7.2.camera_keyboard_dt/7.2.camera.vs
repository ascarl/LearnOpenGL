#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器；用键盘更新后的 View 把世界转换到相机坐标系。
// 输入输出：model/view/projection 组成 MVP，TexCoord 插值后供片段阶段采样。
// 坐标空间：deltaTime 只参与 CPU 相机位置更新，Shader 接收的是更新完成后的观察矩阵。

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoord;

out vec2 TexCoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
	gl_Position = projection * view * model * vec4(aPos, 1.0f);
	TexCoord = vec2(aTexCoord.x, aTexCoord.y);
}