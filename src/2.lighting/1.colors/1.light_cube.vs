#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：光源指示立方体的顶点着色器；仅完成标准 MVP 坐标变换。
// 输入输出：aPos 是共享立方体 VBO 中的位置，三个矩阵决定灯模型在场景中的位置与大小。
// 观察重点：灯立方体只是光源位置的可视化标记，并不参与物体表面的光照计算。
layout (location = 0) in vec3 aPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
	// 与物体使用相同的观察和投影矩阵，使灯标记处于同一个三维场景。
	gl_Position = projection * view * model * vec4(aPos, 1.0);
}