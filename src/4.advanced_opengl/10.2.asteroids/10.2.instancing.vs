#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：非实例化小行星基线的普通顶点着色器，每次 draw 通过 uniform model 接收一个对象变换。
// 输入输出：位置与 UV 来自模型 Mesh，TexCoords 传给片段阶段；projection/view/model 完成 MVP 变换。
// 对比重点：model 不是实例 attribute，CPU 必须在每颗小行星绘制前重新上传它。
layout (location = 0) in vec3 aPos;
layout (location = 2) in vec2 aTexCoords;

out vec2 TexCoords;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

void main()
{
    TexCoords = aTexCoords;
    gl_Position = projection * view * model * vec4(aPos, 1.0f); 
}