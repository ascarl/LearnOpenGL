#version 420 core
// LearnOpenGL 中文导读
// 着色阶段：不透明 Pass 的顶点着色器，每个输入顶点执行一次。
// 输入输出：读取模型位置和 CPU 提供的 MVP；直接输出裁剪空间 gl_Position，不向片段阶段传递额外属性。
// 核心算法：MVP 把局部空间平面变换到裁剪空间，深度随后写入与透明 Pass 共享的深度附件。
// 平台要求：GLSL 4.20 / OpenGL 4.2；macOS 系统 OpenGL 最高 4.1，无法直接运行本示例。

// shader inputs
layout (location = 0) in vec3 position;

// mvp matrix
uniform mat4 mvp;

void main()
{
	gl_Position = mvp * vec4(position, 1.0f);
}