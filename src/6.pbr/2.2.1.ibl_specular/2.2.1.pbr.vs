#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：PBR 顶点着色器，输出 UV、世界空间位置与世界空间法线。
// 输入输出：model 生成 WorldPos，CPU 提供的 normalMatrix 变换 Normal，view/projection 生成裁剪位置。
// 核心算法：直接光与 IBL 的 N、V、L、R 都在世界空间计算，避免 BRDF 点积混用坐标系。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;

out vec2 TexCoords;
out vec3 WorldPos;
out vec3 Normal;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;
uniform mat3 normalMatrix;

void main()
{
    TexCoords = aTexCoords;
    WorldPos = vec3(model * vec4(aPos, 1.0));
    Normal = normalMatrix * aNormal;   

    gl_Position =  projection * view * vec4(WorldPos, 1.0);
}