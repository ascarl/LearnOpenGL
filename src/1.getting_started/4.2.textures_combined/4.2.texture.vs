#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器；把矩形位置送入裁剪空间，并向片段阶段传递 UV。
// 输入输出：location 0/1/2 分别为位置、颜色、纹理坐标；本例最终着色只消费 TexCoord。
// 坐标空间：位置无需矩阵变换，UV 在两个三角形上连续插值供两张纹理共用。

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