#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：正常场景 Pass 的片段着色器，采样立方体或地面二维纹理。
// 输入输出：TexCoords 为插值纹理坐标，texture1 由 CPU 绑定，FragColor 写入颜色附件。
// 数据关系：第一 Pass 的可见立方体片段同时触发模板替换操作，为后续轮廓 Pass 留下掩码。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture1;

void main()
{    
    FragColor = texture(texture1, TexCoords);
}