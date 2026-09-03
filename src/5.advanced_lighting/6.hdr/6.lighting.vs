#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：HDR 场景 Pass 顶点着色器，输出世界空间位置、法线与 UV。
// 输入输出：inverse_normals 翻转隧道内壁法线；normalMatrix 保持缩放后的方向正确。
// Pass 依赖：这些世界空间量进入照明片段阶段，其大范围线性结果将写入 RGBA16F 附件。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;

out VS_OUT {
    vec3 FragPos;
    vec3 Normal;
    vec2 TexCoords;
} vs_out;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

uniform bool inverse_normals;

void main()
{
    vs_out.FragPos = vec3(model * vec4(aPos, 1.0));   
    vs_out.TexCoords = aTexCoords;
    
    vec3 n = inverse_normals ? -aNormal : aNormal;
    
    mat3 normalMatrix = transpose(inverse(mat3(model)));
    vs_out.Normal = normalize(normalMatrix * n);
    
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}