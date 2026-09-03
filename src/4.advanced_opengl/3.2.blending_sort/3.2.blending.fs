#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器，输出纹理原始 RGBA，交由固定功能混合单元进行颜色合成。
// 输入输出：TexCoords 采样 texture1；FragColor 的 alpha 成为 glBlendFunc 配置中的源混合因子。
// 数据关系：目标颜色来自当前颜色附件已有内容，所以相同片段集合采用不同绘制顺序会得到不同结果。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture1;

void main()
{             
    FragColor = texture(texture1, TexCoords);
}