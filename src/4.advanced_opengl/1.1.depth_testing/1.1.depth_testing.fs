#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器，为通过深度测试的片段输出二维纹理颜色。
// 输入输出：插值后的 TexCoords 用于采样 texture1，结果写入默认帧缓冲的颜色附件。
// 观察重点：深度比较由固定功能管线完成，本 Shader 不需要显式读取或写入深度值。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture1;

void main()
{    
    FragColor = texture(texture1, TexCoords);
}