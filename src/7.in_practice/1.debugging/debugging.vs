#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器，把立方体局部坐标变换到裁剪空间。
// 输入输出：location 0/1 分别读取位置与纹理坐标，projection、model 为 uniform，TexCoords 传给片段阶段。
// 坐标空间：此例没有独立 view 矩阵，model 同时把物体平移到观察范围内，再由 projection 完成透视投影。

layout (location = 0) in vec3 position;
layout (location = 1) in vec2 texCoords;

uniform mat4 projection;
uniform mat4 model;

out vec2 TexCoords;

void main()
{
    gl_Position = projection * model * vec4(position, 1.0f);
    TexCoords = texCoords;
}