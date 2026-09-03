#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：Bloom 场景 Pass 顶点着色器，输出世界空间位置、法线和 UV。
// 输入输出：model/view/projection 决定裁剪位置，逆转置矩阵生成世界空间法线。
// Pass 依赖：片段阶段将同一光照结果写入 HDR 场景色与高亮提取两个 MRT 附件。
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

void main()
{
    vs_out.FragPos = vec3(model * vec4(aPos, 1.0));   
    vs_out.TexCoords = aTexCoords;
        
    mat3 normalMatrix = transpose(inverse(mat3(model)));
    vs_out.Normal = normalize(normalMatrix * aNormal);
    
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}