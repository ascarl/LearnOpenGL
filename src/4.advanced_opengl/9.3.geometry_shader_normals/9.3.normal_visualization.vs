#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：法线可视化 Pass 的顶点着色器，输出观察空间位置与观察空间法线。
// 输入输出：aPos/aNormal 来自模型；gl_Position 暂存 view*model 后的位置，normal block 传给几何阶段。
// 核心算法：逆转置 normalMatrix 正确处理缩放；projection 延后到几何阶段，便于在线性观察空间延伸法线。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;

out VS_OUT {
    vec3 normal;
} vs_out;

uniform mat4 view;
uniform mat4 model;

void main()
{
    mat3 normalMatrix = mat3(transpose(inverse(view * model)));
    vs_out.normal = vec3(vec4(normalMatrix * aNormal, 0.0));
    gl_Position = view * model * vec4(aPos, 1.0); 
}