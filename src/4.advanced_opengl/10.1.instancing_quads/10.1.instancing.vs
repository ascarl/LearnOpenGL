#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：实例化顶点着色器，把共享四边形顶点与每实例二维偏移组合成最终裁剪空间位置。
// 输入输出：aPos/aColor 每顶点前进，aOffset 因 divisor=1 每实例前进；fColor 传给片段阶段。
// 核心算法：无需 model 矩阵，直接在 NDC 风格坐标上相加即可把 100 个副本分布到网格位置。
layout (location = 0) in vec2 aPos;
layout (location = 1) in vec3 aColor;
layout (location = 2) in vec2 aOffset;

out vec3 fColor;

void main()
{
    fColor = aColor;
    gl_Position = vec4(aPos + aOffset, 0.0, 1.0);
}