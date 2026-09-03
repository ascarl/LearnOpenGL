#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：镜面贴图示例的顶点着色器，延续位置、法线和纹理坐标三条数据通路。
// 输入输出：FragPos/Normal 位于世界空间，TexCoords 位于模型纹理空间并在三角形内插值。
// 本节新增：片段阶段将用同一 TexCoords 分别采样漫反射贴图与镜面贴图。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;

out vec3 FragPos;
out vec3 Normal;
out vec2 TexCoords;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    FragPos = vec3(model * vec4(aPos, 1.0));
    // 法线矩阵只变换方向；纹理坐标不受模型矩阵影响。
    Normal = mat3(transpose(inverse(model))) * aNormal;  
    TexCoords = aTexCoords;
    
    gl_Position = projection * view * vec4(FragPos, 1.0);
}