#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器；用每帧变化的 View 表现环绕相机。
// 输入输出：Model 定位对象，View 来自 lookAt，Projection 建立透视，TexCoord 传给片段阶段。
// MVP 顺序：projection*view*model*position；相机运动通过观察矩阵统一影响场景中所有顶点。

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