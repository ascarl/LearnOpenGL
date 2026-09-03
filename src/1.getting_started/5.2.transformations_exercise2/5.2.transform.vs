#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器；使用每次绘制前更新的 transform 变换同一份矩形顶点。
// 与基础示例的精确差异：Shader 与 5.1 完全相同；C++ 通过两次上传 transform 分别实现旋转和脉冲缩放。
// 矩阵顺序：gl_Position=transform*position，组合矩阵仍按从右向左作用于列向量。

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoord;

out vec2 TexCoord;

uniform mat4 transform;

void main()
{
	gl_Position = transform * vec4(aPos, 1.0);
	TexCoord = vec2(aTexCoord.x, aTexCoord.y);
}