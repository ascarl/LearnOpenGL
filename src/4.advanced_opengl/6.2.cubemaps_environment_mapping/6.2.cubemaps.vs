#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：环境映射物体的顶点着色器，输出世界空间位置和正确变换后的世界空间法线。
// 输入输出：aPos/aNormal 来自 VAO，Position/Normal 交给片段阶段，MVP 生成最终裁剪空间位置。
// 核心算法：法线使用 model 逆转置矩阵，避免非均匀缩放破坏法线与表面的垂直关系。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;

out vec3 Normal;
out vec3 Position;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    Normal = mat3(transpose(inverse(model))) * aNormal;
    Position = vec3(model * vec4(aPos, 1.0));
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}