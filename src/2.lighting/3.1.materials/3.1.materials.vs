#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：材质示例的顶点着色器；继续为逐片段 Phong 光照准备世界空间几何量。
// 输入输出：位置与法线由 VBO 输入，FragPos/Normal 插值后供材质片段 Shader 使用。
// 本节新增：光照公式不再直接依赖单一 objectColor，而由片段阶段的 Material 参数控制反射特性。
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
    // 逆转置法线矩阵让法线在任意 model 缩放下仍适合世界空间光照计算。
    Normal = mat3(transpose(inverse(model))) * aNormal;  
    
    gl_Position = projection * view * vec4(FragPos, 1.0);
}