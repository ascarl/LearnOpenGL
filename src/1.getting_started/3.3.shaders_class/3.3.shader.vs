#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器；逐顶点读取 location 0 的位置和 location 1 的颜色。
// 输入输出：aPos 直接写入裁剪空间 gl_Position，aColor 通过 ourColor 传给片段阶段并在图元内插值。
// 坐标空间：aPos 先作为裁剪空间 xyz 写入 gl_Position；本例 w=1，透视除法后的 NDC xyz 才与 aPos 数值相同，且不使用 MVP。

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aColor;

out vec3 ourColor;

void main()
{
    gl_Position = vec4(aPos, 1.0);
    ourColor = aColor;
}