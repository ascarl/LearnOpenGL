#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：MSAA 场景片段着色器，为覆盖样本输出固定绿色。
// 输入输出：无纹理输入；默认帧缓冲带多样本附件时，FragColor 写入被图元覆盖的样本。
// 数据关系：多样本目标会在显示前把同一像素的样本解析为最终颜色；单样本目标则没有这一效果。
out vec4 FragColor;

void main()
{
    FragColor = vec4(0.0, 1.0, 0.0, 1.0);
} 