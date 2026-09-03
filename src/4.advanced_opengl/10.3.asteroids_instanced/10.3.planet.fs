#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：中心行星的片段着色器，采样其模型漫反射纹理。
// 输入输出：TexCoords 为插值 UV，texture_diffuse1 由行星 Model 绘制流程绑定。
// 数据关系：行星与小行星使用不同 Program，但最终都写入同一默认颜色和深度附件。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture_diffuse1;

void main()
{
    FragColor = texture(texture_diffuse1, TexCoords);
}