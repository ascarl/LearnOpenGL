#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：爆炸模型的片段着色器，在几何位移后仍按原模型 UV 采样漫反射纹理。
// 输入输出：TexCoords 由几何 Shader 逐顶点转发，texture_diffuse1 来自模型材质绑定。
// 数据关系：几何阶段只改变位置，不改变纹理坐标，因此每块飞散三角形保留原有表面图案。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture_diffuse1;

void main()
{
    FragColor = texture(texture_diffuse1, TexCoords);
}

