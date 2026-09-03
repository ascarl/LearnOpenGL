#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：红色立方体的片段着色器，输出固定不透明红色。
// 输入输出：无需插值输入或纹理；FragColor 写入默认帧缓冲颜色附件。
// 数据关系：它与其他三个片段 Shader 搭配同一个 UBO 顶点 Shader，仅最终颜色不同。
out vec4 FragColor;

void main()
{
    FragColor = vec4(1.0, 0.0, 0.0, 1.0);
}