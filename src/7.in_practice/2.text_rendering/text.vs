#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器，把按像素生成的文字四边形投影到裁剪空间。
// 输入输出：vertex.xy 是屏幕位置、vertex.zw 是字形纹理坐标，TexCoords 传给片段阶段。
// 坐标空间：projection 为二维正交投影，因此 CPU 可直接用像素位置排版文字。

layout (location = 0) in vec4 vertex; // <vec2 pos, vec2 tex>
out vec2 TexCoords;

uniform mat4 projection;

void main()
{
    gl_Position = projection * vec4(vertex.xy, 0.0, 1.0);
    TexCoords = vertex.zw;
}