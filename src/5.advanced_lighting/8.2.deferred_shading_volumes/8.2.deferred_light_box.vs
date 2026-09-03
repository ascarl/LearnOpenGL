#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：灯光可视化前向 Pass 顶点着色器，用相机 MVP 绘制代表点光源的小立方体。
// 输入输出：只写 gl_Position，不读取 G-buffer；模型矩阵由 CPU 放置并缩放每个灯箱。
// Pass 依赖：CPU 先把 G-buffer 深度复制回默认帧缓冲，使灯箱接受场景深度遮挡。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

void main()
{
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}