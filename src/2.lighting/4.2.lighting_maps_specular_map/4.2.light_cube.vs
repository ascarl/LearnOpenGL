#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：镜面贴图示例中灯标记的顶点着色器。
// 输入输出：位置属性经 MVP 变换写入 gl_Position，不向片段阶段传额外数据。
// 观察重点：主物体新增镜面纹理不会改变灯标记的数据布局。
layout (location = 0) in vec3 aPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    // 使用与主场景一致的 view/projection。
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}