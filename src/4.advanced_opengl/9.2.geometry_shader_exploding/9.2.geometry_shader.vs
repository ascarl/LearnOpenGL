#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：爆炸效果的顶点着色器，把模型顶点完整变换到裁剪空间并转发纹理坐标。
// 输入输出：aPos/aTexCoords 来自 Mesh；VS_OUT block 与几何阶段同名结构匹配。
// 数据关系：后续几何 Shader 直接在当前裁剪坐标上计算面方向与位移，然后重新发射三角形。
layout (location = 0) in vec3 aPos;
layout (location = 2) in vec2 aTexCoords;

out VS_OUT {
    vec2 texCoords;
} vs_out;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

void main()
{
    vs_out.texCoords = aTexCoords;
    gl_Position = projection * view * model * vec4(aPos, 1.0); 
}