#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：自发光贴图练习中灯标记的顶点着色器。
// 输入输出：只读取位置属性并应用 MVP 变换，输出裁剪空间位置。
// 观察重点：自发光是主物体材质的一部分，与灯标记自身的绘制程序无关。
layout (location = 0) in vec3 aPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    // 灯标记仍位于同一相机观察的三维场景中。
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}