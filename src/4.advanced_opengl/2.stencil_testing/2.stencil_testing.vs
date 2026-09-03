#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器，供正常纹理 Pass 和纯色轮廓 Pass 复用。
// 输入输出：位置与纹理坐标来自 VAO，TexCoords 传给正常片段 Shader，MVP 变换生成裁剪空间位置。
// 观察重点：轮廓 Pass 由 CPU 放大 model 矩阵；模板判断发生在固定功能逐片段测试阶段。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    TexCoords = aTexCoords;    
    gl_Position = projection * view * model * vec4(aPos, 1.0f);
}