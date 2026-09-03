#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：多采样场景 Pass 的顶点着色器，执行立方体的普通 MVP 变换。
// 输入输出：aPos 来自场景 VAO，model/view/projection 生成裁剪空间位置。
// 渲染目标：光栅化结果写入当前绑定的 4x MSAA Framebuffer，而不是窗口默认帧缓冲。
layout (location = 0) in vec3 aPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}