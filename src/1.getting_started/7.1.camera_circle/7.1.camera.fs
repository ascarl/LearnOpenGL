#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器；为环绕相机看到的立方体输出双纹理混合色。
// 输入输出：两个 sampler 读取单元 0/1，FragColor 写入默认帧缓冲颜色附件。
// 核心算法：相机只改变顶点位置与可见关系，不改变这里固定 80%/20% 的材质颜色。

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