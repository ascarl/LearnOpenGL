#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器，将地板顶点变换到裁剪空间并输出世界空间位置、法线与 UV。
// 输入输出：model/view/projection 决定位置，法线逆转置矩阵保证世界空间方向正确。
// 核心算法：片段阶段的光源距离、法线和观察方向都基于这里输出的同一世界空间。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;

out VS_OUT {
    vec3 FragPos;
    vec3 Normal;
    vec2 TexCoords;
} vs_out;

uniform mat4 projection;
uniform mat4 view;

void main()
{
    vs_out.FragPos = aPos;
    vs_out.Normal = aNormal;
    vs_out.TexCoords = aTexCoords;
    gl_Position = projection * view * vec4(aPos, 1.0);
}