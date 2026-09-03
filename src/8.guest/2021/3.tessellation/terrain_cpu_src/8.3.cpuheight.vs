#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：CPU 高度地形的顶点着色器，每个已展开的高度图网格顶点执行一次。
// 输入输出：aPos 已包含 CPU 计算的世界高度；Height 传给片段阶段着色，Position 位于观察空间，gl_Position 位于裁剪空间。
// 核心算法：GPU 不再采样高度图，只执行 model/view/projection 并转发顶点 Y 值。

layout (location = 0) in vec3 aPos;

out float Height;
out vec3 Position;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    Height = aPos.y;
    Position = (view * model * vec4(aPos, 1.0)).xyz;
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}