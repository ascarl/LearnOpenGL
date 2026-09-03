#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：多光源场景中点光源标记的片段着色器。
// 输入输出：无材质输入，固定向默认颜色附件输出白色。
// 观察重点：四次绘制只改变 model 矩阵；该 Shader 不显示各点光源实际的彩色强度参数。
out vec4 FragColor;

void main()
{
    // 所有点光源标记保持统一白色，便于定位而不混入光照模型。
    FragColor = vec4(1.0); // set all 4 vector values to 1.0
}