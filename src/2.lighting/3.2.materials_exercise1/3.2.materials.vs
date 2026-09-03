#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：材质练习的顶点着色器；沿用世界空间逐片段光照的数据通路。
// 输入输出：aPos/aNormal 生成世界空间 FragPos/Normal，供片段阶段套用预设材质参数。
// 本练习新增：CPU 选择青色塑料材质参数，借相同 Shader 比较材质系数对画面的影响。
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
    // 法线使用 model 的逆转置矩阵，和 FragPos 一起保持在世界空间。
    Normal = mat3(transpose(inverse(model))) * aNormal;  
    
    gl_Position = projection * view * vec4(FragPos, 1.0);
}