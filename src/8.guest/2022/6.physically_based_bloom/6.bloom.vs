#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：HDR 场景 Pass 的顶点着色器，每个场景网格顶点执行一次。
// 输入输出：读取局部位置/法线/UV，输出世界空间位置、逆转置矩阵变换的法线与 UV，并生成裁剪空间位置。
// 数据流：世界空间属性供片段阶段计算四个点光源，MVP 结果供固定管线光栅化。

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