#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：点光源深度 Pass 的顶点着色器，只把顶点变换到世界空间。
// 输入输出：gl_Position 暂存 model*aPos；下一几何阶段将同一世界坐标投影到六个立方体面。
// Pass 依赖：不在此处使用相机矩阵，因为六个 shadowMatrices 由几何着色器逐层应用。
layout (location = 0) in vec3 aPos;

uniform mat4 model;

void main()
{
    gl_Position = model * vec4(aPos, 1.0);
}