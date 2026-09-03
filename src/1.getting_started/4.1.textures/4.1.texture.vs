#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器；读取位置、颜色和二维纹理坐标三组顶点属性。
// 输入输出：位置直接进入裁剪空间，TexCoord 传给片段阶段；ourColor 在当前片段着色器中未参与最终颜色。
// 坐标空间：aTexCoord 保持 [0,1] UV 语义，光栅化器会为矩形内部片段插值。

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