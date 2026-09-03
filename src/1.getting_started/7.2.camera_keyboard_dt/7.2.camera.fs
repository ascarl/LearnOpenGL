#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器；输出自由移动相机所观察到的纹理表面。
// 输入输出：TexCoord 采样两个 sampler，FragColor 写入当前颜色附件。
// 核心算法：固定纹理混合与相机速度无关；深度测试在后续固定功能阶段处理遮挡。

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