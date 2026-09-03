#version 420 core
// LearnOpenGL 中文导读
// 着色阶段：透明合成 Pass 的全屏顶点着色器，每个全屏三角形顶点执行一次。
// 输入输出：position 已位于 NDC，直接形成覆盖目标纹理的全屏几何；片段阶段用 gl_FragCoord 定位 texel。
// 核心算法：省略矩阵和 UV 插值；仅当 viewport 与 framebuffer attachment 尺寸一致时，gl_FragCoord 才与 accum/reveal texel 一一对应，窗口缩放或 HiDPI 可能破坏此前提。
// 平台要求：GLSL 4.20 / OpenGL 4.2；macOS 系统 OpenGL 最高 4.1，无法直接运行本示例。

// shader inputs
layout (location = 0) in vec3 position;

void main()
{
	gl_Position = vec4(position, 1.0f);
}