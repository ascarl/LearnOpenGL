#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：软边聚光灯示例的顶点着色器；继续输出世界空间光照数据与 UV。
// 输入输出：FragPos/Normal 参与光照和锥角计算，TexCoords 供材质贴图采样。
// 本节新增：片段阶段在内外截止角之间平滑插值强度，顶点阶段无需新增属性。
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
    // 法线矩阵让世界空间法线在模型非均匀缩放后仍保持正确方向。
    Normal = mat3(transpose(inverse(model))) * aNormal;  
    TexCoords = aTexCoords;
    
    gl_Position = projection * view * vec4(FragPos, 1.0);
}