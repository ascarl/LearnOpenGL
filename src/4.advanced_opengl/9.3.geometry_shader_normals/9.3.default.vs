#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：第一 Pass 的普通模型顶点着色器，执行完整 MVP 变换并转发 UV。
// 输入输出：aPos/aTexCoords 来自模型 Mesh，TexCoords 供默认片段 Shader 采样漫反射纹理。
// 渲染目标：生成纹理模型的裁剪空间三角形，先写默认帧缓冲颜色与深度。
layout (location = 0) in vec3 aPos;
layout (location = 2) in vec2 aTexCoords;

out vec2 TexCoords;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

void main()
{
    TexCoords = aTexCoords;
    gl_Position = projection * view * model * vec4(aPos, 1.0); 
}