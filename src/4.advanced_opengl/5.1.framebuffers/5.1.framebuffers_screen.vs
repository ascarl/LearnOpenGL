#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：屏幕 Pass 的顶点着色器，直接接收 NDC 坐标，不再应用 model/view/projection。
// 输入输出：二维位置生成裁剪空间顶点，纹理坐标传给片段阶段以覆盖离屏颜色纹理。
// 渲染目标：六个顶点形成全屏四边形，后续片段写入窗口默认帧缓冲。
layout (location = 0) in vec2 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

void main()
{
    TexCoords = aTexCoords;
    gl_Position = vec4(aPos.x, aPos.y, 0.0, 1.0); 
}  