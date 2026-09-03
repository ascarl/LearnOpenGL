#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：粒子片段着色器，把粒子纹理与其随生命周期衰减的颜色相乘。
// 输入输出：TexCoords/ParticleColor 来自顶点阶段，sprite 提供粒子图案，color 写入场景颜色附件。
// 混合关系：CPU 绘制此阶段时切换为 SRC_ALPHA、ONE，加法累积令重叠粒子更亮。

in vec2 TexCoords;
in vec4 ParticleColor;
out vec4 color;

uniform sampler2D sprite;

void main()
{
    color = (texture(sprite, TexCoords) * ParticleColor);
}