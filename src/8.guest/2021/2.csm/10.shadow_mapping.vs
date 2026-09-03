#version 410 core
// LearnOpenGL 中文导读
// 着色阶段：最终带阴影场景 Pass 的顶点着色器，每个场景顶点执行一次。
// 输入输出：读取局部位置/法线/UV与 model/view/projection；向片段阶段输出世界空间位置、世界空间法线和 UV。
// 核心算法：世界空间数据供方向光照和 CSM 查询，gl_Position 则完成常规 MVP 裁剪空间变换。

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;

out vec2 TexCoords;

out VS_OUT {
    vec3 FragPos;
    vec3 Normal;
    vec2 TexCoords;
} vs_out;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

void main()
{
    vs_out.FragPos = vec3(model * vec4(aPos, 1.0));
    vs_out.Normal = transpose(inverse(mat3(model))) * aNormal;
    vs_out.TexCoords = aTexCoords;
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}
