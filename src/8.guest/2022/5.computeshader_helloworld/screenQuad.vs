#version 430 core
// LearnOpenGL 中文导读
// 着色阶段：计算结果显示 Pass 的全屏顶点着色器，每个四边形顶点执行一次。
// 输入输出：读取 NDC 位置与 UV，直接输出裁剪空间位置并把 TexCoords 插值给片段阶段。
// 核心算法：建立 Compute Shader 输出纹理与窗口像素之间的全屏映射。
// 平台要求：GLSL 4.30 / OpenGL 4.3；macOS 系统 OpenGL 最高 4.1，无法直接运行本示例。

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

void main()
{
    TexCoords = aTexCoords;
    gl_Position = vec4(aPos, 1.0);
}
