#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：调试用全屏四边形顶点着色器，直接输出裁剪空间位置并透传纹理坐标。
// 输入输出：二维顶点与 UV 不经过相机矩阵，TexCoords 供后续附件可视化片段着色器采样。
// Pass 依赖：它只负责显示前一 Pass 的纹理附件，不参与场景几何或光照计算。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

void main()
{
    TexCoords = aTexCoords;
    gl_Position = vec4(aPos, 1.0);
}