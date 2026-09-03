#version 420 core
// LearnOpenGL 中文导读
// 着色阶段：不透明 Pass 的片段着色器，每个被光栅化且通过测试的片元执行一次。
// 输入输出：uniform color 是线性 RGB 材质色；location 0 输出不透明 RGBA 到 opaqueTexture。
// 核心算法：固定 alpha 为 1，建立后续透明合成所需的背景颜色和深度。
// 平台要求：GLSL 4.20 / OpenGL 4.2；macOS 系统 OpenGL 最高 4.1，无法直接运行本示例。

// shader outputs
layout (location = 0) out vec4 frag;

// material color
uniform vec3 color;

void main()
{
	frag = vec4(color, 1.0f);
}