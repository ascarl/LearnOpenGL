#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：聚光灯章节保留的灯立方体顶点着色器模板。
// 输入输出：位置经 MVP 变换；本示例的聚光灯绑定相机，不绘制独立灯模型。
// 观察重点：手电筒由 camera.Position 与 camera.Front 定义，灯立方体不能表达其朝向和锥体。
layout (location = 0) in vec3 aPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    // 若复用该模板，仍输出标准裁剪空间坐标。
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}