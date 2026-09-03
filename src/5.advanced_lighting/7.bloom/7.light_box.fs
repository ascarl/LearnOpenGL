#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：灯箱片段着色器，把光源颜色同时写入 HDR 场景附件和条件高亮附件。
// 输入输出：FragColor 对应 MRT 0，BrightColor 对应 MRT 1，lightColor 可大于 1。
// Pass 依赖：亮度阈值与场景材质一致，使发光灯箱也参与后续 Bloom 模糊。
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