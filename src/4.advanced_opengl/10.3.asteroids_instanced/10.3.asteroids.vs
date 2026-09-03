#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：小行星实例化顶点着色器，从四个连续 attribute location 组装每实例 model 矩阵。
// 输入输出：aPos/UV 按顶点前进，aInstanceMatrix 按实例前进；projection/view 为整批实例共享 uniform。
// 核心算法：每个实例以自己的矩阵执行 model 变换，同时复用相同 Mesh 顶点和一次绘制命令。
layout (location = 0) in vec3 aPos;
layout (location = 2) in vec2 aTexCoords;
layout (location = 3) in mat4 aInstanceMatrix;

out vec2 TexCoords;

uniform mat4 projection;
uniform mat4 view;

void main()
{
    TexCoords = aTexCoords;
    gl_Position = projection * view * aInstanceMatrix * vec4(aPos, 1.0f); 
}