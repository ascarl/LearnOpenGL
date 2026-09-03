#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：Bloom 最终合成 Pass 的全屏顶点着色器，每个四边形顶点执行一次。
// 输入输出：读取 NDC 位置与 UV，直接输出裁剪空间位置并把 TexCoords 传给最终片段阶段。
// 数据流：scene 与 bloomBlur 两张纹理使用同一屏幕 UV 对齐采样。

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

void main()
{
    TexCoords = aTexCoords;
    gl_Position = vec4(aPos, 1.0);
}