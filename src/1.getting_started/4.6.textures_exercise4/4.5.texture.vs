#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器；把位置送入裁剪空间并传递 UV。
// 与基础示例的精确差异：顶点 Shader 与 4.2 完全相同；交互式变化发生在片段 Shader 的 mixValue。
// 输入输出：TexCoord 经光栅化插值后供两张纹理使用，ourColor 本例不参与最终颜色。

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