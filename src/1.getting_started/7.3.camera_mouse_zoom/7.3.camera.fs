#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器；为鼠标可控相机下的立方体输出纹理颜色。
// 输入输出：TexCoord 采样 texture1/texture2，FragColor 写入默认帧缓冲。
// 核心算法：固定 80%/20% 混合；视角与缩放变化不会改变表面材质计算。

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