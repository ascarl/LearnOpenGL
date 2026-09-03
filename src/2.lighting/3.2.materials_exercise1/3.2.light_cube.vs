#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：材质练习中灯标记的顶点着色器。
// 输入输出：仅有位置属性与 MVP uniform，输出灯立方体的裁剪空间坐标。
// 观察重点：灯标记维持独立 Shader，便于专注观察受光物体材质参数的变化。
layout (location = 0) in vec3 aPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
	// model 决定光源标记的位置和 0.2 倍缩放。
	gl_Position = projection * view * model * vec4(aPos, 1.0);
}