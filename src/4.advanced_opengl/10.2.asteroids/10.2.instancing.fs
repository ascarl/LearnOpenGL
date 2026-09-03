#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：行星和小行星共用的片段着色器，采样模型第一张漫反射纹理。
// 输入输出：TexCoords 来自 Mesh 顶点，texture_diffuse1 由 Model 绘制时绑定，FragColor 写默认颜色附件。
// 数据关系：性能差异来自 CPU 提交和顶点变换方式，片段着色工作与实例化版本基本相同。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture_diffuse1;

void main()
{
    FragColor = texture(texture_diffuse1, TexCoords);
}