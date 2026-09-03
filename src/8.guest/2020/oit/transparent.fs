#version 420 core
// LearnOpenGL 中文导读
// 着色阶段：Weighted Blended OIT 的透明片段累积阶段，每个可见透明片元执行一次。
// 输入输出：uniform color 提供颜色和透明度；location 0 只输出当前片元的加权预乘 RGBA，location 1 的 reveal 只输出当前片元 alpha。
// 核心算法：Shader 只计算两个附件的源值；CPU 用 glBlendFunci 配置混合状态，GPU 固定功能混合阶段再对 accum 做加法、对 reveal 附件按 dst * (1 - alpha) 连乘。
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
	// 此处尚未跨片元累积；attachment 0 的 src + dst 由 GPU 固定功能混合阶段完成。
	accum = vec4(color.rgb * color.a, color.a) * weight;
	
	// store pixel revealage threshold
	// reveal 只是当前片元 alpha；attachment 1 从清屏值 1 开始在固定功能阶段累计剩余透射率。
	reveal = color.a;
}