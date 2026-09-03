#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：天空盒顶点着色器，把立方体位置同时作为 Cubemap 采样方向输出。
// 输入输出：view 已移除平移，projection 保留透视；TexCoords 是方向向量而不是常规二维 UV。
// 核心算法：pos.xyww 令裁剪空间 z 等于 w，透视除法后深度恒为 1，使天空盒位于最远平面。
layout (location = 0) in vec3 aPos;

out vec3 TexCoords;

uniform mat4 projection;
uniform mat4 view;

void main()
{
    TexCoords = aPos;
    vec4 pos = projection * view * vec4(aPos, 1.0);
    gl_Position = pos.xyww;
}  