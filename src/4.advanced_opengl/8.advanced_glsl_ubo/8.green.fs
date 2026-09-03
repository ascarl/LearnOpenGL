#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：绿色立方体的片段着色器，输出固定不透明绿色。
// 输入输出：无需插值输入或纹理；FragColor 写入默认帧缓冲颜色附件。
// 数据关系：Program 独立但顶点阶段的 projection/view 通过相同 UBO 绑定点共享。
out vec4 FragColor;

void main()
{
    FragColor = vec4(0.0, 1.0, 0.0, 1.0);
}