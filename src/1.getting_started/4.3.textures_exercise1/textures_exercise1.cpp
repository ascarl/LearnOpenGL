// LearnOpenGL 中文导读
// 文件性质：这是 4.2 片段着色器的练习答案片段，不是独立 C++ 程序。
// 与基础示例的精确差异：采样第二张纹理时将 U 改为 1.0-TexCoord.x，仅水平翻转笑脸；第一张纹理和 0.2 混合权重不变。
// 观察重点：只变换一个 sampler 的采样坐标，不会翻转矩形几何或容器纹理。

#version 330 core
out vec4 FragColor;

in vec3 ourColor;
in vec2 TexCoord;

uniform sampler2D ourTexture1;
uniform sampler2D ourTexture2;

void main()
{
    // 关键步骤：仅为 ourTexture2 构造镜像 UV，mix 的两个输入因此可使用不同采样方向。
    FragColor = mix(texture(ourTexture1, TexCoord), texture(ourTexture2, vec2(1.0 - TexCoord.x, TexCoord.y)), 0.2);
}