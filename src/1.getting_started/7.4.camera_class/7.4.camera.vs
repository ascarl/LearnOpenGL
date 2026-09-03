#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器；使用 Camera 类计算出的 View 与 Zoom 派生的 Projection。
// 输入输出：Model/View/Projection 生成裁剪空间位置，TexCoord 传给片段阶段。
// 坐标空间：与 7.3 的公式相同，差异仅是矩阵和相机状态由可复用 Camera 类提供。

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