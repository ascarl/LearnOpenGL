#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：镜面叠加 Pass 的顶点着色器，直接输出位于屏幕顶部的小型 NDC 四边形。
// 输入输出：aPos 已是裁剪空间 xy，aTexCoords 传给片段阶段读取完整镜像纹理。
// 数据关系：无需相机矩阵；四边形顶点范围由 CPU 数据决定镜面在最终画面中的位置和大小。
layout (location = 0) in vec2 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

void main()
{
    TexCoords = aTexCoords;
    gl_Position = vec4(aPos.x, aPos.y, 0.0, 1.0); 
}  