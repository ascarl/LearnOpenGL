#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：多采样场景 Pass 的片段着色器，为立方体输出固定绿色。
// 输入输出：FragColor 写入多采样颜色附件中被图元覆盖的样本，深度测试同步更新多采样 rbo。
// 数据关系：这些样本不能直接作为后处理 sampler2D，下一 Pass 会通过 blit 解析到普通纹理。
out vec4 FragColor;

void main()
{
    FragColor = vec4(0.0, 1.0, 0.0, 1.0);
} 