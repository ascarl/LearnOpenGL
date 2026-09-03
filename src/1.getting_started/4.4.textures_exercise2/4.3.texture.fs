#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器；以同一插值 UV 采样两张纹理并固定按 80%/20% 混合。
// 与基础示例的精确差异：Shader 与 4.2 完全相同；重复或边缘拉伸由 C++ 设置的 GL_REPEAT/GL_CLAMP_TO_EDGE 决定。
// 输入输出：两个 sampler 指向纹理单元 0/1，FragColor 写入当前颜色附件。

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