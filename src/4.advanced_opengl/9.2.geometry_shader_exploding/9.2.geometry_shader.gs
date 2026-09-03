#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：几何着色器，每次读取一个 triangle，输出位移后的同一个三顶点 triangle_strip。
// 输入输出：gl_in 提供三个裁剪空间位置，gs_in 提供对应 UV；TexCoords 与新位置逐顶点发射。
// 核心算法：边向量叉积得到当前坐标中的面方向，sin(time) 产生 [0,1] 位移系数，三个顶点沿同一方向移动。
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
    // 使用同一图元的两条边求叉积，三个顶点共享该面方向，避免三角形在移动中被拉伸。
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