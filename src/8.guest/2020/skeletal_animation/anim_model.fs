#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：骨骼动画模型的片段着色器，每个通过测试的模型片元执行一次。
// 输入输出：接收蒙皮后几何插值得到的 TexCoords，采样 texture_diffuse1，并输出到默认颜色附件。
// 核心算法：本示例把教学重点留给顶点蒙皮，因此片段阶段只呈现模型漫反射纹理，不计算光照。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture_diffuse1;

void main()
{    
    FragColor = texture(texture_diffuse1, TexCoords);
}
