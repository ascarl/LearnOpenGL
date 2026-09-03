#version 410 core
// LearnOpenGL 中文导读
// 着色阶段：级联深度层调试全屏四边形的顶点着色器，每个四边形顶点执行一次。
// 输入输出：读取已位于 NDC 的位置和 UV，直接输出裁剪空间位置并把 TexCoords 传给片段阶段。
// 核心算法：绕过相机矩阵，使选中的深度纹理数组层覆盖整个窗口便于检查。

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

void main()
{
    TexCoords = aTexCoords;
    gl_Position = vec4(aPos, 1.0);
}
