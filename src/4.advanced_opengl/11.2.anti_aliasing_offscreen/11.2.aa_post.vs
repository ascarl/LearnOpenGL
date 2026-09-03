#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：最终屏幕 Pass 的顶点着色器，直接输出覆盖整个 NDC 的二维四边形。
// 输入输出：aPos 已是裁剪空间 xy，aTexCoords 传给片段阶段读取已解析的 screenTexture。
// 数据关系：它不需要场景矩阵；几何只负责把单采样结果映射到默认帧缓冲全部像素。
layout (location = 0) in vec2 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

void main()
{
    TexCoords = aTexCoords;
    gl_Position = vec4(aPos.x, aPos.y, 0.0, 1.0); 
}  