#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器，为每个输入点传递位置与颜色，不执行相机变换。
// 输入输出：aPos 直接成为裁剪空间点；颜色通过 VS_OUT interface block 传给几何阶段。
// 数据关系：每个顶点对应后续一次 points 几何 Shader 调用，是生成整栋房子的锚点。
layout (location = 0) in vec2 aPos;
layout (location = 1) in vec3 aColor;

out VS_OUT {
    vec3 color;
} vs_out;

void main()
{
    vs_out.color = aColor;
    gl_Position = vec4(aPos.x, aPos.y, 0.0, 1.0); 
}