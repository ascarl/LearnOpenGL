#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：Phong 镜面反射示例的顶点着色器；向片段阶段输出世界空间位置与法线。
// 输入输出：aPos/aNormal 来自 VBO，FragPos 与 Normal 必须处在同一世界坐标系中参与点积。
// 本节新增：使用 model 的逆转置矩阵变换法线，避免非均匀缩放破坏法线与表面的垂直关系。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;

out vec3 FragPos;
out vec3 Normal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    FragPos = vec3(model * vec4(aPos, 1.0));
    // 法线只表示方向，因此取 mat3 去除齐次平移；逆转置负责保持几何正交关系。
    Normal = mat3(transpose(inverse(model))) * aNormal;  
    
    gl_Position = projection * view * vec4(FragPos, 1.0);
}