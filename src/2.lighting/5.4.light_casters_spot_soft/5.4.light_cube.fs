#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：软边聚光示例保留的灯标记片段着色器模板。
// 输入输出：不读取材质或光锥参数，固定输出不透明白色。
// 观察重点：本程序未用于当前相机手电筒的最终绘制。
out vec4 FragColor;

void main()
{
    // 固定灯色便于在其他示例中复用该模板。
    FragColor = vec4(1.0); // set all 4 vector values to 1.0
}