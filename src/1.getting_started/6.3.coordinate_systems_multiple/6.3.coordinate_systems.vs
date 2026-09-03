#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器；每次绘制接收不同 Model，共享 View/Projection 来定位十个立方体。
// 输入输出：MVP 生成裁剪空间位置，TexCoord 传递给片段阶段。
// 核心算法：UV 的 V 分量使用 1.0-aTexCoord.y 翻转，补偿图片坐标与 OpenGL UV 原点方向差异。

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoord;

out vec2 TexCoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    gl_Position = projection * view * model * vec4(aPos, 1.0f);
    TexCoord = vec2(aTexCoord.x, 1.0 - aTexCoord.y);
}