#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：漫反射贴图示例的灯标记片段着色器。
// 输入输出：无纹理输入，直接写入不透明白色到默认颜色附件。
// 观察重点：灯标记不使用物体的 diffuse 贴图。
out vec4 FragColor;

void main()
{
    // 每个灯立方体片段均输出相同颜色。
    FragColor = vec4(1.0); // set all 4 vector values to 1.0
}