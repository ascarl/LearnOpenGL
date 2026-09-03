#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：基础漫反射示例中灯标记的片段着色器。
// 输入输出：无跨阶段输入和 uniform，向默认颜色附件写入不透明白色。
// 观察重点：该输出仅显示点光源位置，实际物体光照由另一片段 Shader 完成。
out vec4 FragColor;

void main()
{
    // vec4(1.0) 同时把 RGB 与 alpha 四个分量设为 1。
    FragColor = vec4(1.0); // set all 4 vector values to 1.0
}