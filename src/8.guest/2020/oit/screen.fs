#version 420 core
// LearnOpenGL 中文导读
// 着色阶段：最终显示 Pass 的片段着色器，每个窗口像素执行一次。
// 输入输出：按插值 UV 采样已经完成 OIT 合成的 screen 纹理，输出不透明颜色到默认帧缓冲。
// 核心算法：该阶段不再参与透明计算，只负责把 opaqueTexture 的 RGB 呈现到屏幕。
// 平台要求：GLSL 4.20 / OpenGL 4.2；macOS 系统 OpenGL 最高 4.1，无法直接运行本示例。

// shader inputs
in vec2 texture_coords;

// shader outputs
layout (location = 0) out vec4 frag;

// screen image
uniform sampler2D screen;

void main()
{
	frag = vec4(texture(screen, texture_coords).rgb, 1.0f);
}