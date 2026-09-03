#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器；位置直接进入裁剪空间，UV 插值后供片段阶段采样。
// 与基础示例的精确差异：Shader 与 4.2 完全相同；练习变化只在 C++ 顶点 UV 扩为 0..2 及纹理环绕参数。
// 输入输出：location 0/1/2 对应位置、颜色、UV；片段阶段实际使用 TexCoord。

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