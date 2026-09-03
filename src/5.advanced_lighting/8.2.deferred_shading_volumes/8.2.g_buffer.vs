#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：延迟着色几何 Pass 顶点着色器，输出世界空间位置、法线和 UV。
// 输入输出：worldPos=model*aPos，Normal 用逆转置模型矩阵；gl_Position 再经 view/projection。
// Pass 依赖：片段阶段把这些几何/材质量写入三个 G-buffer 颜色附件。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;

out vec3 FragPos;
out vec2 TexCoords;
out vec3 Normal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    vec4 worldPos = model * vec4(aPos, 1.0);
    FragPos = worldPos.xyz; 
    TexCoords = aTexCoords;
    
    mat3 normalMatrix = transpose(inverse(mat3(model)));
    Normal = normalMatrix * aNormal;

    gl_Position = projection * view * worldPos;
}