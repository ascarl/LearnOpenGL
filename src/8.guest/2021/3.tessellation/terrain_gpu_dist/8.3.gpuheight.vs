#version 410 core
// LearnOpenGL 中文导读
// 着色阶段：GPU 地形细分管线的顶点阶段，每个 patch 控制点执行一次。
// 输入输出：读取粗网格局部位置和高度图 UV，原样写入 gl_Position/TexCoord 供 TCS 的控制点数组读取。
// 数据流：这里不做 MVP 变换；控制点必须保留在模型空间，供后续 TCS 测距和 TES 位移、投影。

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTex;

out vec2 TexCoord;

void main()
{
    gl_Position = vec4(aPos, 1.0);
    TexCoord = aTex;
}