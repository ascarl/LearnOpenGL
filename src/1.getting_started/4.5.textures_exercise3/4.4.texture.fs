#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器；用相同 UV 采样两张纹理并固定混合。
// 与基础示例的精确差异：Shader 与 4.2 完全相同；像素块效果来自 C++ 的 GL_NEAREST 过滤状态。
// 输入输出：texture1/texture2 为 sampler，FragColor 是默认帧缓冲的最终 RGBA。

out vec4 FragColor;

in vec3 ourColor;
in vec2 TexCoord;

// texture samplers
uniform sampler2D texture1;
uniform sampler2D texture2;

void main()
{
	// linearly interpolate between both textures (80% container, 20% awesomeface)
	FragColor = mix(texture(texture1, TexCoord), texture(texture2, TexCoord), 0.2);
}