#version 330 core
// LearnOpenGL 中文导读
// 文件性质：虽然后缀为 .cpp，但内容是 4.2 片段着色器的练习答案，可作为 GLSL 源码使用。
// 与基础示例的精确差异：第二张纹理改用 1.0-TexCoord.x 水平翻转；sampler 名也从 texture1/texture2 改为 ourTexture1/ourTexture2。
// 接口注意：若直接沿用 4.2 C++ 对 texture1/texture2 的 uniform 设置，名称无法匹配本 Shader，两个新 sampler 不会得到预期的 0/1 单元映射。
// 观察重点：第一张纹理与 0.2 混合权重不变，只对第二张纹理的采样坐标做水平镜像。

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