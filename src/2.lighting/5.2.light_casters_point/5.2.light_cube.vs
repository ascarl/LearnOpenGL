#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：点光源位置标记的顶点着色器。
// 输入输出：共享立方体位置经灯 model、相机 view 和 projection 变换后写入 gl_Position。
// 观察重点：点光源有明确世界空间位置，因此可以用缩小的立方体直观标出。
layout (location = 0) in vec3 aPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    // 灯 model 矩阵平移到 lightPos，并缩放为较小的可视标记。
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}