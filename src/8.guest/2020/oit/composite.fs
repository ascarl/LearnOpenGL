#version 420 core
// LearnOpenGL 中文导读
// 着色阶段：Weighted Blended OIT 的透明合成片段着色器，每个屏幕像素执行一次。
// 输入输出：binding 0/1 分别读取 accum 与 reveal 纹理；location 0 输出恢复后的透明层颜色和覆盖率。
// 核心算法：以累计权重归一化 RGB，并用 1-revealage 得到合成 alpha；无透明贡献时直接 discard。
// 平台要求：GLSL 4.20 / OpenGL 4.2；macOS 系统 OpenGL 最高 4.1，无法直接运行本示例。

// shader outputs
layout (location = 0) out vec4 frag;

// color accumulation buffer
layout (binding = 0) uniform sampler2D accum;

// revealage threshold buffer
layout (binding = 1) uniform sampler2D reveal;

// epsilon number
const float EPSILON = 0.00001f;

// calculate floating point numbers equality accurately
bool isApproximatelyEqual(float a, float b)
{
	return abs(a - b) <= (abs(a) < abs(b) ? abs(b) : abs(a)) * EPSILON;
}

// get the max value between three values
float max3(vec3 v) 
{
	return max(max(v.x, v.y), v.z);
}

void main()
{
	// fragment coordination
	ivec2 coords = ivec2(gl_FragCoord.xy);
	
	// fragment revealage
	float revealage = texelFetch(reveal, coords, 0).r;
	
	// save the blending and color texture fetch cost if there is not a transparent fragment
	if (isApproximatelyEqual(revealage, 1.0f)) 
		discard;
 
	// fragment color
	vec4 accumulation = texelFetch(accum, coords, 0);
	
	// suppress overflow
	if (isinf(max3(abs(accumulation.rgb)))) 
		accumulation.rgb = vec3(accumulation.a);

	// prevent floating point precision bug
	// accum.a 是所有透明片元的加权 alpha 总和，除法把预乘累计值还原为平均透明颜色。
	vec3 average_color = accumulation.rgb / max(accumulation.a, EPSILON);

	// blend pixels
	frag = vec4(average_color, 1.0f - revealage);
}