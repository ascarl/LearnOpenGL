#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：视锥内模型的片段着色器，每个通过深度测试的片元执行一次。
// 输入输出：按 TexCoords 采样 texture_diffuse1，结果写入默认颜色附件。
// 核心算法：渲染结果与未剔除版本保持一致；性能差异来自片段阶段之前已经避免了不可见实体的提交和光栅化。

out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture_diffuse1;

void main()
{    
    FragColor = texture(texture_diffuse1, TexCoords);
}