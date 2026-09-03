#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器，从二维纹理读取立方体表面颜色。
// 输入输出：TexCoords 来自顶点插值，tex 指向当前纹理单元，FragColor 写入默认颜色缓冲。
// 观察重点：该着色器本身保持简单，便于把注意力集中在 CPU 侧 OpenGL 错误与调试回调上。

out vec4 FragColor;
in vec2 TexCoords;

uniform sampler2D tex;

void main()
{
    FragColor = texture(tex, TexCoords);
}