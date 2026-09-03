#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：可视化光源立方体的 HDR 片段着色器，每个光源片元执行一次。
// 输入输出：uniform lightColor 同时写完整场景 FragColor，并按亮度阈值选择性写入 Bloom 的 BrightColor 附件。
// 核心算法：高强度光源通常超过阈值，因此既显示发光体，也为后续模糊提供稳定的高亮种子。

layout (location = 0) out vec4 FragColor;
layout (location = 1) out vec4 BrightColor;

in VS_OUT {
    vec3 FragPos;
    vec3 Normal;
    vec2 TexCoords;
} fs_in;

uniform vec3 lightColor;

void main()
{           
    FragColor = vec4(lightColor, 1.0);
    float brightness = dot(FragColor.rgb, vec3(0.2126, 0.7152, 0.0722));
    if(brightness > 1.0)
        BrightColor = vec4(FragColor.rgb, 1.0);
	else
		BrightColor = vec4(0.0, 0.0, 0.0, 1.0);
}