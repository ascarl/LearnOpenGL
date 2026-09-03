#version 430 core
// LearnOpenGL 中文导读
// 着色阶段：计算着色器；每个 work group 启动 10x10x1 个 invocation，每个全局调用负责一个输出 texel。
// 输入输出：uniform t 控制动画，binding 0 的 rgba32f image2D 与 CPU 的 glBindImageTexture 对应，imageStore 写入最终颜色。
// 数据流：gl_GlobalInvocationID.xy 直接作为整数 texel 坐标；组数乘局部尺寸覆盖 1000x1000 图像，本阶段不使用共享内存。
// 平台要求：GLSL 4.30 / OpenGL 4.3；macOS 系统 OpenGL 最高 4.1，无法直接运行本示例。

layout (local_size_x = 10, local_size_y = 10, local_size_z = 1) in;

// ----------------------------------------------------------------------------
//
// uniforms
//
// ----------------------------------------------------------------------------

layout(rgba32f, binding = 0) uniform image2D imgOutput;

layout (location = 0) uniform float t;                 /** Time */

// ----------------------------------------------------------------------------
//
// functions
//
// ----------------------------------------------------------------------------

void main() {
	vec4 value = vec4(0.0, 0.0, 0.0, 1.0);
	// 每个 invocation 的全局二维编号唯一定位输出图像中的一个像素，不发生 invocation 间写冲突。
	ivec2 texelCoord = ivec2(gl_GlobalInvocationID.xy);
	float speed = 100;
	// the width of the texture
	float width = 1000;

	value.x = mod(float(texelCoord.x) + t * speed, width) / (gl_NumWorkGroups.x * gl_WorkGroupSize.x);
	value.y = float(texelCoord.y)/(gl_NumWorkGroups.y*gl_WorkGroupSize.y);
	// imageStore 是无过滤、按整数坐标的图像写入；CPU 屏障后同一纹理才进入屏幕采样 Pass。
	imageStore(imgOutput, texelCoord, value);
}