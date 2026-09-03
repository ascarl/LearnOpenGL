#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器；输出 Camera 类示例中每个立方体的双纹理颜色。
// 输入输出：两个 sampler 对应纹理单元 0/1，FragColor 写入当前颜色附件。
// 核心算法：与 7.3 完全相同的固定权重混合，Camera 封装不会改变片段着色路径。

out vec4 FragColor;

in vec2 TexCoord;

// texture samplers
uniform sampler2D texture1;
uniform sampler2D texture2;

void main()
{
	// linearly interpolate between both textures (80% container, 20% awesomeface)
	FragColor = mix(texture(texture1, TexCoord), texture(texture2, TexCoord), 0.2);
}