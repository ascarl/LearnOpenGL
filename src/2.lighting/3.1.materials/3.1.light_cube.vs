#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：材质示例中灯立方体的顶点着色器，仅负责 MVP 变换。
// 输入输出：读取共享 VBO 的位置，灯的 model 矩阵由 CPU 根据 lightPos 构造。
// 观察重点：灯标记保持白色，而 CPU 生成的动态 lightColor 只控制受光物体的 Light 分量。
layout (location = 0) in vec3 aPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
	// 将灯标记置于与受光物体一致的观察、投影空间。
	gl_Position = projection * view * model * vec4(aPos, 1.0);
}