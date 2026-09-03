#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：中心行星的普通顶点着色器，不使用实例属性。
// 输入输出：aPos/UV 来自行星 Mesh，projection/view/model 均由 CPU 作为 uniform 设置。
// 数据关系：行星只绘制一次，保留普通路径可突出小行星批量实例化的数据组织差异。
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