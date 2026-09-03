#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：四个颜色程序共用的顶点着色器，展示 GLSL interface block 与 UBO。
// 输入输出：aPos 来自 VAO；Matrices 中 projection/view 来自绑定点 0 的共享 UBO，model 仍是每物体 uniform。
// 内存布局：std140 规定稳定的对齐规则，两个 mat4 各占连续四个 vec4 列，CPU 可按相同偏移写入。
layout (location = 0) in vec3 aPos;

layout (std140) uniform Matrices
{
    mat4 projection;
    mat4 view;
};
uniform mat4 model;

void main()
{
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}  