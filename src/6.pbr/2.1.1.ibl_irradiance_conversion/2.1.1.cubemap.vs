#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：IBL 捕获顶点着色器，从 Cubemap 中心把立方体顶点投影到当前面。
// 输入输出：WorldPos 作为环境采样方向，capture projection/view 逐面生成 gl_Position。
// Pass 依赖：本程序仅在经纬 HDR 图转换为 envCubemap 时使用该顶点路径，并通过六个 capture view 逐面渲染。
layout (location = 0) in vec3 aPos;

out vec3 WorldPos;

uniform mat4 projection;
uniform mat4 view;

void main()
{
    WorldPos = aPos;
    gl_Position =  projection * view * vec4(WorldPos, 1.0);
}