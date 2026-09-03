#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：CPU 高度地形的片段着色器，每个地形片元执行一次。
// 输入输出：接收插值后的模型/局部空间高度 Height，归一化为灰度并写入默认颜色附件。
// 核心算法：(Height + 16) / 32 使高度 -16 映射为黑、16 映射为白；更高的输入会在归一化颜色附件中饱和。
// 空间说明：main.cpp 当前使用单位 model，所以 Height 的数值在本例中恰好也等于世界空间高度。


out vec4 FragColor;

in float Height;

void main()
{
    float h = (Height + 16)/32.0f;	// shift and scale the height into a grayscale value
    FragColor = vec4(h, h, h, 1.0);
}