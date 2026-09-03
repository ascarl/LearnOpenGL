#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：自发光贴图练习的顶点着色器；继续传递世界空间位置/法线和纹理坐标。
// 输入输出：属性 0/1/2 对应位置、法线、UV，三个 varying 在光栅化时进行插值。
// 本练习新增：片段阶段会增加 emission 采样，顶点数据布局与前一光照贴图示例相同。
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
    // 法线用于光照方向计算，UV 同时服务 diffuse、specular 和 emission 三张贴图。
    Normal = mat3(transpose(inverse(model))) * aNormal;  
    TexCoords = aTexCoords;
    
    gl_Position = projection * view * vec4(FragPos, 1.0);
}