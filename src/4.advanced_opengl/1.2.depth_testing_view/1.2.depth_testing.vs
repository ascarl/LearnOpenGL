#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器，仅负责将局部空间位置变换到裁剪空间。
// 输入输出：aPos 来自顶点缓冲，model/view/projection 分别描述模型、相机和透视投影变换。
// 核心算法：投影矩阵生成的 z/w 在透视除法后成为非线性 NDC 深度，随后映射到窗口深度范围。
layout (location = 0) in vec3 aPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}