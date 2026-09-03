#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：MSAA 场景的顶点着色器，执行普通 MVP 变换。
// 输入输出：aPos 来自立方体 VAO，model/view/projection 生成裁剪空间位置。
// 观察重点：MSAA 不改变顶点接口；它在图元光栅化后为像素内多个样本计算覆盖与深度。
layout (location = 0) in vec3 aPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}