#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：几何着色器，每次读取一个 triangle，输出位移后的同一个三顶点 triangle_strip。
// 输入输出：gl_in 提供三个裁剪空间位置，gs_in 提供对应 UV；TexCoords 与新位置逐顶点发射。
// 核心算法：裁剪空间 xyz 边向量的叉积只作为风格化方向；sin(time) 产生 [0,1] 位移系数。
layout (triangles) in;
layout (triangle_strip, max_vertices = 3) out;

in VS_OUT {
    vec2 texCoords;
} gs_in[];

out vec2 TexCoords; 

uniform float time;

vec4 explode(vec4 position, vec3 normal)
{
    float magnitude = 2.0;
    vec3 direction = normal * ((sin(time) + 1.0) / 2.0) * magnitude; 
    return position + vec4(direction, 0.0);
}

vec3 GetNormal()
{
    // 函数名沿用教程；输入已在裁剪空间，叉积结果受投影影响，不是物体、世界或观察空间的真实面法线。
    // 三个顶点虽加同一裁剪空间偏移，但 w 可能不同；透视除法后的 NDC 位移不同，不能保证图元不拉伸。
    vec3 a = vec3(gl_in[0].gl_Position) - vec3(gl_in[1].gl_Position);
    vec3 b = vec3(gl_in[2].gl_Position) - vec3(gl_in[1].gl_Position);
    return normalize(cross(a, b));
}

void main() {    
    vec3 normal = GetNormal();

    gl_Position = explode(gl_in[0].gl_Position, normal);
    TexCoords = gs_in[0].texCoords;
    EmitVertex();
    gl_Position = explode(gl_in[1].gl_Position, normal);
    TexCoords = gs_in[1].texCoords;
    EmitVertex();
    gl_Position = explode(gl_in[2].gl_Position, normal);
    TexCoords = gs_in[2].texCoords;
    EmitVertex();
    EndPrimitive();
}