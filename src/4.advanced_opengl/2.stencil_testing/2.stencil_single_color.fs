#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：轮廓 Pass 的片段着色器，为放大后的立方体输出统一颜色。
// 输入输出：不需要纹理输入；FragColor 仅在模板值不等于 1 的片段位置写入默认颜色附件。
// 数据关系：该 Pass 读取第一 Pass 建立的模板掩码，不写模板，并通过纯色形成物体外沿。
out vec4 FragColor;

void main()
{
    FragColor = vec4(0.04, 0.28, 0.26, 1.0);
}