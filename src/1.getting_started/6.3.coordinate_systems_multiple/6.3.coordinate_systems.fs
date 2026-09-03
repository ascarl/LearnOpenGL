#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器；所有立方体实例共用同一双纹理材质。
// 输入输出：插值 TexCoord 采样 texture1/texture2，FragColor 写入当前颜色附件。
// 核心算法：固定权重 mix 不区分实例；实例差异仅由顶点阶段的 Model 矩阵产生。

out vec4 FragColor;

in vec2 TexCoord;

uniform sampler2D texture1;
uniform sampler2D texture2;

void main()
{
    FragColor = mix(texture(texture1, TexCoord), texture(texture2, TexCoord), 0.2);
}