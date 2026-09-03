#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：点光源深度 Pass 的几何着色器，把每个输入三角形复制到 Cubemap 六个面。
// 输入输出：shadowMatrices[0..5] 是光源位置处六个 90° 视锥；gl_Layer 选择对应深度 Cubemap 面。
// Pass 依赖：FragPos 保持世界空间位置，片段阶段据此写片元到点光源的径向距离。
layout (triangles) in;
layout (triangle_strip, max_vertices=18) out;

uniform mat4 shadowMatrices[6];

out vec4 FragPos; // FragPos from GS (output per emitvertex)

void main()
{
    for(int face = 0; face < 6; ++face)
    {
        gl_Layer = face; // built-in variable that specifies to which face we render.
        for(int i = 0; i < 3; ++i) // for each triangle's vertices
        {
            FragPos = gl_in[i].gl_Position;
            gl_Position = shadowMatrices[face] * FragPos;
            EmitVertex();
        }    
        EndPrimitive();
    }
} 