#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：基础漫反射示例的顶点着色器；首次把表面位置和法线传给片段阶段。
// 输入输出：aPos/aNormal 来自交错 VBO，FragPos 位于世界空间，Normal 当前仍是模型空间原值。
// 本节新增：为环境光与 Lambert 漫反射准备几何信息；当前 model 为单位矩阵，后续才引入法线矩阵。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;

out vec3 FragPos;
out vec3 Normal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    // 片段光照需要世界空间位置，插值后即可构造“片段指向光源”的方向。
    FragPos = vec3(model * vec4(aPos, 1.0));
    // 本小节未变换法线，仅因示例物体没有非单位模型变换才保持正确。
    Normal = aNormal;  
    
    gl_Position = projection * view * vec4(FragPos, 1.0);
}