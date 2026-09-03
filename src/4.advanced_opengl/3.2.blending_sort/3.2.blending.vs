#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器，为不透明物体和透明窗户生成裁剪空间位置。
// 输入输出：aPos/aTexCoords 来自顶点缓冲，TexCoords 传给片段阶段，MVP uniform 描述当前物体与相机。
// 观察重点：透明排序在 CPU 绘制顺序中完成，顶点变换本身与不透明物体没有区别。
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