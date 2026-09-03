#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器，为几何阶段生成的房屋三角形输出插值颜色。
// 输入输出：fColor 来自几何 Shader；墙体沿用原点颜色，屋顶顶点被设为白色并在三角形内插值。
// 渲染目标：最终颜色写入默认帧缓冲，深度来自几何阶段发射的裁剪空间位置。
out vec4 FragColor;

in vec3 fColor;

void main()
{
    FragColor = vec4(fColor, 1.0);   
}