#version 420 core
// LearnOpenGL 中文导读
// 着色阶段：透明累积 Pass 的顶点着色器，每个透明平面顶点执行一次。
// 输入输出：读取局部空间 position 与 MVP，输出裁剪空间位置；透明颜色由片段着色器 uniform 提供。
// 核心算法：只负责几何变换，透明顺序无关近似发生在片段输出和附件混合阶段。
// 平台要求：GLSL 4.20 / OpenGL 4.2；macOS 系统 OpenGL 最高 4.1，无法直接运行本示例。

// shader inputs
layout (location = 0) in vec3 position;

// model * view * projection matrix
uniform mat4 mvp;

void main()
{
	gl_Position = mvp * vec4(position, 1.0f);
}