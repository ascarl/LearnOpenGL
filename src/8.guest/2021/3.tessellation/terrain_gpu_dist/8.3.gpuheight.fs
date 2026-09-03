#version 410 core
// LearnOpenGL 中文导读
// 着色阶段：GPU 细分地形的片段着色器，每个光栅化地形片元执行一次。
// 输入输出：接收 TES 输出并插值的 Height，把归一化灰度写入默认颜色附件。
// 核心算法：(Height + 16) / 64 将位移高度映射为观察用灰度，便于看到细分后连续的高度变化。


in float Height;

out vec4 FragColor;

void main()
{
    float h = (Height + 16)/64.0f;
    FragColor = vec4(h, h, h, 1.0);
}