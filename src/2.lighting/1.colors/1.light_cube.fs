#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：光源指示立方体的片段着色器。
// 输入输出：无需纹理或 uniform，直接向默认颜色附件输出纯白色。
// 观察重点：白色立方体只帮助观察 lightPos，不代表真实光源几何体或发光计算。
out vec4 FragColor;

void main()
{
    // alpha 同样设为 1，使灯标记保持不透明。
    FragColor = vec4(1.0); // set all 4 vector values to 1.0
}