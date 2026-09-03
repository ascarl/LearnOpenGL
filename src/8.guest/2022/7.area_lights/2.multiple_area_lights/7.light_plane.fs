#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：多面光源可视化片段着色器，每个发光矩形片元执行一次。
// 输入输出：CPU 为当前灯设置 lightColor，着色器直接输出不透明恒定色。
// 核心算法：仅用于标记各灯的位置、朝向和颜色；LTC 受光计算发生在 multi_area_light.fs。


out vec4 color;
uniform vec3 lightColor;

void main()
{
	color = vec4(lightColor, 1.0f);
}
