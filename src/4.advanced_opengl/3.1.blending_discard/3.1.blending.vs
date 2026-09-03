#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器，将立方体、地面和植被四边形统一变换到裁剪空间。
// 输入输出：位置和纹理坐标来自 VAO，TexCoords 插值后交给片段阶段决定采样与丢弃。
// 核心算法：projection * view * model 完成局部空间到裁剪空间的完整 MVP 变换。
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