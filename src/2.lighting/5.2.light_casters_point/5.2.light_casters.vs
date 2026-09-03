#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：点光源示例的顶点着色器；向片段阶段提供世界空间几何量与纹理坐标。
// 输入输出：FragPos/Normal 用于计算到有限 light.position 的方向和距离，TexCoords 用于材质采样。
// 本节新增：片段光照开始依赖每个片段到点光源的位置差，进而产生距离衰减。
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
    // 法线和片段位置都变换到世界空间，避免混用坐标系导致点积错误。
    Normal = mat3(transpose(inverse(model))) * aNormal;  
    TexCoords = aTexCoords;
    
    gl_Position = projection * view * vec4(FragPos, 1.0);
}