#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器；为经过 MVP 变换的纹理矩形合成两张纹理。
// 输入输出：TexCoord 是插值 UV，texture1/texture2 是纹理单元映射，FragColor 写入颜色附件。
// 核心算法：固定 80%/20% 混合；颜色计算不依赖当前顶点所在的坐标空间。

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