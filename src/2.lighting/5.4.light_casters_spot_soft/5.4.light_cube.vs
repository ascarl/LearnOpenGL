#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：软边聚光章节保留的灯标记顶点着色器模板。
// 输入输出：位置通过 MVP 变换写入 gl_Position；相机手电筒路径不会绘制它。
// 观察重点：聚光灯的位置和方向每帧取自 Camera，实际效果应从光锥边缘而非立方体判断。
layout (location = 0) in vec3 aPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    // 模板保持与其他灯类型一致的坐标变换接口。
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}