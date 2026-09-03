#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器；从两个纹理单元采样并合成当前片段。
// 输入输出：texture1/texture2 由 CPU 映射到单元 0/1，FragColor 写入默认帧缓冲颜色附件。
// 核心算法：mix(a,b,0.2) 计算 0.8*a+0.2*b，使笑脸以 20% 权重叠加到容器。

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