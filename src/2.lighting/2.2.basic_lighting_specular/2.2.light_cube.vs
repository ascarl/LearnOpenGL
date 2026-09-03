#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：镜面反射示例中灯立方体的顶点着色器。
// 输入输出：位置经 model/view/projection 变换后写入 gl_Position，不产生额外 varying。
// 观察重点：灯标记与光照 Shader 解耦，可直观看到用于计算 lightPos 的世界空间位置。
layout (location = 0) in vec3 aPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
	// 灯模型矩阵包含位移和缩放，view/projection 与主物体保持一致。
	gl_Position = projection * view * model * vec4(aPos, 1.0);
}