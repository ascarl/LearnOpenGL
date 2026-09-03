#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：漫反射示例中灯立方体的顶点着色器。
// 输入输出：只读取位置并应用 MVP；不需要法线，因为该程序输出恒定灯色。
// 观察重点：灯标记与受光立方体共享 VBO，但由独立 VAO 和 Shader 解释、绘制。
layout (location = 0) in vec3 aPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
	// 输出裁剪空间坐标，供固定的光栅化阶段生成灯立方体片段。
	gl_Position = projection * view * model * vec4(aPos, 1.0);
}