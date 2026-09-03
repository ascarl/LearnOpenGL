#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器；输出双纹理混合颜色。
// 与基础示例的精确差异：片段 Shader 与 6.1 相同；片段是否写入颜色附件由固定功能深度测试决定。
// 核心算法：先按 UV 计算颜色，随后管线用片段深度决定保留或丢弃该结果。

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