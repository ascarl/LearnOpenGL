#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器；使用 Model/View/Projection 把立方体顶点送入裁剪空间。
// 与基础示例的精确差异：变换公式与 6.1 相同；本节关键变化在 C++ 侧启用深度测试。
// 输入输出：TexCoord 供片段采样，gl_Position 的深度经透视除法后参与深度比较。

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