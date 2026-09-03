#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：场景图模型的顶点着色器，每个被提交节点的每个顶点执行一次。
// 输入输出：读取局部空间位置/法线/UV；model 是 CPU 场景图累计出的世界矩阵，UV 传给片段阶段。
// 核心算法：按 projection * view * model 把节点局部坐标变换到裁剪空间，父子关系已折叠在 model 中。

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