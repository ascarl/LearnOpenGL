#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器；接收光栅化器插值后的 ourColor。
// 输入输出：in vec3 ourColor 来自顶点阶段，FragColor 写入当前帧缓冲的颜色附件。
// 核心算法：补齐 alpha=1 形成不透明 RGBA；三角形内部颜色由三个顶点颜色线性插值。

out vec4 FragColor;

in vec3 ourColor;

void main()
{
    FragColor = vec4(ourColor, 1.0f);
}