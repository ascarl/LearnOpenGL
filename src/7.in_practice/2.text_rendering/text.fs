#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器，把单通道字形覆盖率着色为指定文字颜色。
// 输入输出：text 提供 GL_RED 覆盖率，textColor 提供 RGB，color 写入当前颜色缓冲。
// 核心算法：采样红通道作为 Alpha，再与文字颜色相乘；配合 SRC_ALPHA 混合得到平滑字形边缘。

in vec2 TexCoords;
out vec4 color;

uniform sampler2D text;
uniform vec3 textColor;

void main()
{    
    vec4 sampled = vec4(1.0, 1.0, 1.0, texture(text, TexCoords).r);
    color = vec4(textColor, 1.0) * sampled;
}