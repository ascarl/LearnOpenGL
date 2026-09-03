#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器；传递位置与纹理坐标。
// 与基础示例的精确差异：Shader 与 4.2 完全相同；练习在 C++ 中把 UV 收窄为 0.45..0.55 并启用最近邻过滤。
// 输入输出：插值 TexCoord 只覆盖纹理中心区域，因此该区域会被放大到整个矩形。

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aColor;
layout (location = 2) in vec2 aTexCoord;

out vec3 ourColor;
out vec2 TexCoord;

void main()
{
	gl_Position = vec4(aPos, 1.0);
	ourColor = aColor;
	TexCoord = vec2(aTexCoord.x, aTexCoord.y);
}