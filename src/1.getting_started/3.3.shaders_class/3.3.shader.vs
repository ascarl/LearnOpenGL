#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器；逐顶点读取 location 0 的位置和 location 1 的颜色。
// 输入输出：aPos 直接写入裁剪空间 gl_Position，aColor 通过 ourColor 传给片段阶段并在图元内插值。
// 坐标空间：本例输入位置已是 NDC 范围，齐次 w=1，不再应用模型、观察或投影矩阵。

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aColor;

out vec3 ourColor;

void main()
{
    gl_Position = vec4(aPos, 1.0);
    ourColor = aColor;
}