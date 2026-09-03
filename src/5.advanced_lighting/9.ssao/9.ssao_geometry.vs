#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：SSAO 几何 Pass 顶点着色器，将位置与法线统一变换到视空间。
// 输入输出：FragPos=view*model*aPos，normalMatrix 基于 view*model；invertedNormals 支持房间内壁。
// Pass 依赖：SSAO 的深度比较、采样半球与最终光照都假定 G-buffer 位置/法线位于视空间。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;

out vec3 FragPos;
out vec2 TexCoords;
out vec3 Normal;

uniform bool invertedNormals;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    vec4 viewPos = view * model * vec4(aPos, 1.0);
    FragPos = viewPos.xyz; 
    TexCoords = aTexCoords;
    
    mat3 normalMatrix = transpose(inverse(mat3(view * model)));
    Normal = normalMatrix * (invertedNormals ? -aNormal : aNormal);
    
    gl_Position = projection * viewPos;
}