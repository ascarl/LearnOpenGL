#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：延迟光照 Pass 全屏顶点着色器，直接输出裁剪空间四边形与 UV。
// 输入输出：aPos/aTexCoords 不经过相机变换；每个屏幕片元将对应读取一组 G-buffer 数据。
// Pass 依赖：只在几何 Pass 完成 gPosition、gNormal、gAlbedoSpec 后执行。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

void main()
{
    TexCoords = aTexCoords;
    gl_Position = vec4(aPos, 1.0);
}