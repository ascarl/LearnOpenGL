#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：方向光示例的顶点着色器；为逐片段光照输出世界空间位置、法线和纹理坐标。
// 输入输出：属性 0/1/2 分别是位置、法线、UV，model/view/projection 完成空间变换。
// 本节新增：片段阶段改用没有空间位置和距离衰减的方向光，顶点数据通路保持不变。
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
    // 逆转置法线矩阵确保 Normal 与世界空间 FragPos、光方向处于一致坐标系。
    Normal = mat3(transpose(inverse(model))) * aNormal;  
    TexCoords = aTexCoords;
    
    gl_Position = projection * view * vec4(FragPos, 1.0);
}