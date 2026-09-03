#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：实例化四边形的片段着色器，输出顶点颜色在三角形内部的插值结果。
// 输入输出：fColor 对每个副本都来自同一套共享顶点颜色，FragColor 写入默认帧缓冲。
// 数据关系：实例属性只改变位置，因此所有实例复用完全相同的颜色布局。
out vec4 FragColor;

in vec3 fColor;

void main()
{
    FragColor = vec4(fColor, 1.0);
}