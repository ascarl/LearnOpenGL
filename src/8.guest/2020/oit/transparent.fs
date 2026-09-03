#version 420 core
// LearnOpenGL 中文导读
// 着色阶段：Weighted Blended OIT 的透明片段累积阶段，每个可见透明片元执行一次。
// 输入输出：uniform color 提供颜色和透明度；location 0 写加权预乘颜色 accum，location 1 写 revealage。
// 核心算法：权重同时考虑 alpha 与窗口空间深度；CPU 对两个附件分别执行加法累积和剩余透射率连乘。
// 平台要求：GLSL 4.20 / OpenGL 4.2；macOS 系统 OpenGL 最高 4.1，无法直接运行本示例。

// shader outputs
layout (location = 0) out vec4 accum;
layout (location = 1) out float reveal;

// material color
uniform vec4 color;

void main()
{
	// weight function
	// 权重让高 alpha、靠近相机的片元贡献更大，并用 clamp 抑制数值过小或溢出。
	float weight = clamp(pow(min(1.0, color.a * 10.0) + 0.01, 3.0) * 1e8 * pow(1.0 - gl_FragCoord.z * 0.9, 3.0), 1e-2, 3e3);
	
	// store pixel color accumulation
	accum = vec4(color.rgb * color.a, color.a) * weight;
	
	// store pixel revealage threshold
	reveal = color.a;
}