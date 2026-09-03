#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：蓝色立方体的片段着色器，输出固定不透明蓝色。
// 输入输出：无需插值输入或纹理；FragColor 写入默认帧缓冲颜色附件。
// 数据关系：颜色 Program 切换不会改变绑定点 0 上的 UBO，顶点变换继续读取共享矩阵。
out vec4 FragColor;

void main()
{
    FragColor = vec4(0.0, 0.0, 1.0, 1.0);
}