#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：方向光章节保留的灯立方体顶点着色器模板。
// 输入输出：位置经过 MVP 变换；方向光本身没有有限位置，因此主程序不会绘制此灯标记。
// 观察重点：方向光以统一 direction 照射全场景，不能由一个小立方体准确表示。
layout (location = 0) in vec3 aPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    // 若复用该程序绘制标记，仍按标准 MVP 产生裁剪空间位置。
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}