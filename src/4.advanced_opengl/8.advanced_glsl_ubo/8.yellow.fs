#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：黄色立方体的片段着色器，输出固定不透明黄色。
// 输入输出：无需插值输入或纹理；FragColor 写入默认帧缓冲颜色附件。
// 数据关系：每个 Program 只更新自己的 model，而相机相关矩阵来自共享 Matrices block。
out vec4 FragColor;

void main()
{
    FragColor = vec4(1.0, 1.0, 0.0, 1.0);
}