#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：法线线段的片段着色器，为几何 Shader 生成的线统一输出黄色。
// 输入输出：无需纹理或 varying；FragColor 写入与第一 Pass 相同的默认颜色附件。
// 数据关系：线段仍参与深度测试，因此表面后方的法线片段会被第一 Pass 的深度值遮挡。
out vec4 FragColor;

void main()
{
    FragColor = vec4(1.0, 1.0, 0.0, 1.0);
}

