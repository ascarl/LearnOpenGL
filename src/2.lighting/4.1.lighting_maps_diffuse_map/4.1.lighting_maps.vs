#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：漫反射贴图示例的顶点着色器；新增纹理坐标通路并保留世界空间光照数据。
// 输入输出：aPos/aNormal/aTexCoords 分别位于属性 0/1/2，输出经插值的 FragPos、Normal、TexCoords。
// 本节新增：材质颜色从逐顶点对应的二维纹理读取，使表面细节不再受单一 uniform 颜色限制。
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
    // 世界空间法线用于光照，纹理坐标保持模型资源定义的二维参数空间。
    Normal = mat3(transpose(inverse(model))) * aNormal;  
    TexCoords = aTexCoords;
    
    gl_Position = projection * view * vec4(FragPos, 1.0);
}