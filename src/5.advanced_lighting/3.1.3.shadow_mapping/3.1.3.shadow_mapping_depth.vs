#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：方向光深度 Pass 的顶点着色器，把物体顶点从模型空间变换到光源裁剪空间。
// 输入输出：gl_Position=lightSpaceMatrix*model*position；该 Pass 不需要颜色 varying。
// Pass 依赖：光栅化后的窗口深度写入 depthMap，供相机光照 Pass 做最近遮挡深度比较。
layout (location = 0) in vec3 aPos;

uniform mat4 lightSpaceMatrix;
uniform mat4 model;

void main()
{
    gl_Position = lightSpaceMatrix * model * vec4(aPos, 1.0);
}