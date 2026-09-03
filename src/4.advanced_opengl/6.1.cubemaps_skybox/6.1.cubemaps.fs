#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：普通场景片段着色器，采样二维容器纹理并写入默认颜色附件。
// 输入输出：TexCoords 来自插值后的场景顶点数据，texture1 与天空盒 samplerCube 是不同纹理类型。
// 数据关系：它先建立前景颜色和深度，天空盒 Pass 不会覆盖这些较近片段。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture1;

void main()
{    
    FragColor = texture(texture1, TexCoords);
}