#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：硬边聚光示例保留的灯标记片段着色器模板。
// 输入输出：固定输出白色，无纹理或光照输入。
// 观察重点：当前主程序把光源绑定相机并注释掉灯立方体绘制，因此本 Shader 仅作结构对照。
out vec4 FragColor;

void main()
{
    // 如果启用灯模型绘制，它会显示为不透明白色。
    FragColor = vec4(1.0); // set all 4 vector values to 1.0
}