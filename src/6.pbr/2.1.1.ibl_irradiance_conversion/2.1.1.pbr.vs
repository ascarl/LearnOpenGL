#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：PBR 顶点着色器，输出 UV、世界空间位置与世界空间法线。
// 输入输出：model 生成 WorldPos，CPU 提供的 normalMatrix 变换 Normal，view/projection 生成裁剪位置。
// 数据流：WorldPos/Normal 供片段阶段构造直接光的世界空间 N、V、L，TexCoords 仅透传；PBR 物体仍用常量环境项，envCubemap 只供背景 Pass。
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