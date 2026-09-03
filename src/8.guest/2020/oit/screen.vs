#version 420 core
// LearnOpenGL 中文导读
// 着色阶段：最终显示 Pass 的全屏顶点着色器，每个全屏三角形顶点执行一次。
// 输入输出：接收 NDC position 与 UV，把 texture_coords 插值给片段阶段并直接输出裁剪空间位置。
// 核心算法：建立最终离屏颜色纹理到窗口后备缓冲的一一映射。
// 平台要求：GLSL 4.20 / OpenGL 4.2；macOS 系统 OpenGL 最高 4.1，无法直接运行本示例。

// shader inputs
layout (location = 0) in vec3 position;
layout (location = 1) in vec2 uv;

// shader outputs
out vec2 texture_coords;

void main()
{
	texture_coords = uv;

	gl_Position = vec4(position, 1.0f);
}