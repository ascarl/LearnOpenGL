#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：实例化小行星的片段着色器，为所有副本采样共享岩石漫反射纹理。
// 输入输出：TexCoords 来自各 Mesh 的共享顶点，texture_diffuse1 在整批绘制前绑定一次。
// 数据关系：实例矩阵只影响顶点位置；材质采样逻辑不需要知道当前 instance ID。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture_diffuse1;

void main()
{
    FragColor = texture(texture_diffuse1, TexCoords);
}