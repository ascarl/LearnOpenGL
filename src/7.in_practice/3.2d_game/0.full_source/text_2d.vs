#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：游戏文字顶点着色器，把动态生成的单字形四边形投影到屏幕。
// 输入输出：vertex.xy 是像素位置、vertex.zw 是字形纹理坐标，projection 为左上原点正交矩阵。
// 坐标空间：不使用 model/view；CPU 已完成逐字符排版，Shader 只负责从屏幕坐标到裁剪空间的转换。

layout (location = 0) in vec4 vertex; // <vec2 pos, vec2 tex>
out vec2 TexCoords;

uniform mat4 projection;

void main()
{
    gl_Position = projection * vec4(vertex.xy, 0.0, 1.0);
    TexCoords = vertex.zw;
} 