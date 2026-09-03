#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器，把局部空间顶点依次变换到世界、观察和裁剪空间。
// 输入输出：位置和纹理坐标来自 VAO；纹理坐标传给片段阶段，model/view/projection 由 CPU 每帧设置。
// 核心算法：gl_Position 的透视除法与视口变换最终产生窗口深度，供固定功能深度测试使用。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    TexCoords = aTexCoords;    
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}