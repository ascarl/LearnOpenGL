#version 410 core
// LearnOpenGL 中文导读
// 着色阶段：级联体积可视化的片段着色器，每个调试三角形片元执行一次。
// 输入输出：uniform color 区分不同级联并携带透明度，结果写入默认颜色附件与原场景混合。
// 核心算法：固定色输出让级联空间边界可见，不参与阴影采样或光照计算。

out vec4 FragColor;

uniform vec4 color;

void main()
{             
    FragColor = color;
}
