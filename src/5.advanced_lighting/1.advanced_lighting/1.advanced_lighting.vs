#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器，把物体坐标变换到裁剪空间，并向后传递世界空间位置、法线和纹理坐标。
// 输入输出：aPos/aNormal/aTexCoords 经 model、view、projection 处理；Normal 使用逆转置矩阵避免非均匀缩放失真。
// 核心算法：所有光照向量在世界空间构造，为片段阶段比较 Phong 与 Blinn-Phong 保持同一空间基准。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;

// declare an interface block; see 'Advanced GLSL' for what these are.
out VS_OUT {
    vec3 FragPos;
    vec3 Normal;
    vec2 TexCoords;
} vs_out;

uniform mat4 projection;
uniform mat4 view;

void main()
{
    vs_out.FragPos = aPos;
    vs_out.Normal = aNormal;
    vs_out.TexCoords = aTexCoords;
    gl_Position = projection * view * vec4(aPos, 1.0);
}