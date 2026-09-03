#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：第一 Pass 的片段着色器，输出模型漫反射纹理颜色。
// 输入输出：TexCoords 来自默认顶点 Shader，texture_diffuse1 由 Model/Mesh 绘制流程绑定。
// 数据关系：该 Pass 建立模型表面和深度，第二 Pass 在其上叠加几何 Shader 生成的法线线段。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture_diffuse1;

void main()
{
    FragColor = texture(texture_diffuse1, TexCoords);
}

