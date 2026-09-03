#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：视锥剔除后可见模型的顶点着色器，每个实际提交的顶点执行一次。
// 输入输出：读取局部位置/法线/UV和实体世界矩阵；输出裁剪空间位置与供纹理采样的 UV。
// 核心算法：GPU 不再判断可见性；CPU 已用世界空间包围体完成粗粒度剔除，本阶段只处理保留下来的实体。

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;

out vec2 TexCoords;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    TexCoords = aTexCoords;    
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}