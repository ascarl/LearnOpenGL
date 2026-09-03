#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器；两次绘制共用相同的双纹理混合逻辑。
// 与基础示例的精确差异：片段 Shader 与 5.1 完全相同，第二个容器的差异只来自顶点变换 uniform。
// 输入输出：TexCoord 采样单元 0/1 的纹理，固定按 80%/20% 输出 FragColor。

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