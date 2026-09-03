#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：场景图节点模型的片段着色器，每个通过测试的片元执行一次。
// 输入输出：接收插值 UV，采样节点共享模型的 texture_diffuse1，并写入默认颜色附件。
// 核心算法：所有节点复用相同材质采样，画面差异完全来自场景图为各节点生成的世界变换。

out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture_diffuse1;

void main()
{    
    FragColor = texture(texture_diffuse1, TexCoords);
}