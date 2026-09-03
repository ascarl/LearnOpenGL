#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：普通场景顶点着色器，把带二维纹理坐标的立方体变换到裁剪空间。
// 输入输出：aPos/aTexCoords 来自场景 VAO，TexCoords 交给二维纹理片段 Shader，MVP 由 CPU 设置。
// 渲染顺序：该场景先写颜色和深度，随后天空盒只在剩余最远深度区域通过测试。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    TexCoords = aTexCoords;    
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}