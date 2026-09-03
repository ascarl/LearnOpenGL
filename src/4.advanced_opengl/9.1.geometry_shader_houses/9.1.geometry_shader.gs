#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：几何着色器，以单个 point 为输入，输出最多五个顶点的 triangle_strip。
// 输入输出：gl_in[0] 给出锚点位置，gs_in[0].color 给出墙体颜色；fColor 随每个发射顶点传给片段阶段。
// 核心算法：围绕锚点依次发射矩形四角和屋顶顶点；条带自动组装三角形，EndPrimitive 结束本栋房子。
layout (points) in;
layout (triangle_strip, max_vertices = 5) out;

in VS_OUT {
    vec3 color;
} gs_in[];

out vec3 fColor;

void build_house(vec4 position)
{    
    // EmitVertex 会提交当前所有输出值；修改 gl_Position/fColor 后再次调用即可形成下一个条带顶点。
    fColor = gs_in[0].color; // gs_in[0] since there's only one input vertex
    gl_Position = position + vec4(-0.2, -0.2, 0.0, 0.0); // 1:bottom-left   
    EmitVertex();   
    gl_Position = position + vec4( 0.2, -0.2, 0.0, 0.0); // 2:bottom-right
    EmitVertex();
    gl_Position = position + vec4(-0.2,  0.2, 0.0, 0.0); // 3:top-left
    EmitVertex();
    gl_Position = position + vec4( 0.2,  0.2, 0.0, 0.0); // 4:top-right
    EmitVertex();
    gl_Position = position + vec4( 0.0,  0.4, 0.0, 0.0); // 5:top
    fColor = vec3(1.0, 1.0, 1.0);
    EmitVertex();
    EndPrimitive();
}

void main() {    
    build_house(gl_in[0].gl_Position);
}