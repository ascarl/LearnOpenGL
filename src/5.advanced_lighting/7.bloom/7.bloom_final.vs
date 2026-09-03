#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：Bloom 屏幕 Pass 顶点着色器，直接绘制全屏四边形并透传 UV。
// 输入输出：aPos 已在裁剪空间，不使用相机矩阵；TexCoords 查询高亮、乒乓或场景附件。
// Pass 依赖：具体输入由片段阶段决定，顶点阶段在模糊和最终合成 Pass 间复用。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

void main()
{
    TexCoords = aTexCoords;
    gl_Position = vec4(aPos, 1.0);
}