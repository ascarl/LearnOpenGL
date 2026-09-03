#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：镜面贴图示例的灯标记片段着色器。
// 输入输出：不采样任何材质纹理，固定输出白色到默认颜色附件。
// 观察重点：镜面贴图只影响受光物体，不影响光源位置可视化。
out vec4 FragColor;

void main()
{
    // 白色灯标记保持不透明。
    FragColor = vec4(1.0); // set all 4 vector values to 1.0
}