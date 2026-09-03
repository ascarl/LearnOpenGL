#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：传统高斯模糊 Pass 的全屏顶点着色器，每个四边形顶点执行一次。
// 输入输出：读取 NDC 位置与 UV，直接输出裁剪空间位置并把 TexCoords 插值给片段阶段。
// 数据流：同一全屏几何在两个 ping-pong FBO 之间反复绘制，只有输入/输出纹理交替。

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

void main()
{
    TexCoords = aTexCoords;
    gl_Position = vec4(aPos, 1.0);
}