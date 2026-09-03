#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：多光源示例的顶点着色器；一次准备所有光源计算共用的世界空间数据。
// 输入输出：位置、法线、UV 经插值送往片段阶段，多个光源不会重复执行顶点变换。
// 本节新增：片段阶段将同一表面数据分别交给方向光、四个点光源和聚光灯函数并累加。
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
    // 所有灯结构都约定使用世界空间，法线矩阵维持该坐标系一致性。
    Normal = mat3(transpose(inverse(model))) * aNormal;  
    TexCoords = aTexCoords;
    
    gl_Position = projection * view * vec4(FragPos, 1.0);
}