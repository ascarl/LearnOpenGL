#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器；为变换后的矩形计算纹理颜色。
// 输入输出：插值 TexCoord 采样 texture1/texture2，FragColor 写入当前颜色附件。
// 核心算法：几何变换不改变 UV，仍以固定 80% 容器与 20% 笑脸混合。

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