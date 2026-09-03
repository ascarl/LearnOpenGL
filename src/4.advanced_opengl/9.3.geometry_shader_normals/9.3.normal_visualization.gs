#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：法线可视化几何 Shader，每个输入三角形输出三条独立 line_strip 线段。
// 输入输出：gl_in 是三个观察空间顶点，gs_in 是对应观察空间法线；projection 将线段端点变到裁剪空间。
// 核心算法：每条线从顶点出发，沿法线乘 MAGNITUDE 延伸；每两个 EmitVertex 后 EndPrimitive 防止三条线相连。
layout (triangles) in;
layout (line_strip, max_vertices = 6) out;

in VS_OUT {
    vec3 normal;
} gs_in[];

const float MAGNITUDE = 0.2;

uniform mat4 projection;

void GenerateLine(int index)
{
    // 起点和终点都在观察空间构造，最后统一投影可保持方向计算的线性意义。
    gl_Position = projection * gl_in[index].gl_Position;
    EmitVertex();
    gl_Position = projection * (gl_in[index].gl_Position + vec4(gs_in[index].normal, 0.0) * MAGNITUDE);
    EmitVertex();
    EndPrimitive();
}

void main()
{
    GenerateLine(0); // first vertex normal
    GenerateLine(1); // second vertex normal
    GenerateLine(2); // third vertex normal
}