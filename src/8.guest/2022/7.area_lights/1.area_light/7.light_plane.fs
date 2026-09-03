#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：矩形发光面可视化片段着色器，每个光源平面片元执行一次。
// 输入输出：uniform lightColor 直接写到默认颜色附件，不读取 LTC 查找表或材质。
// 核心算法：恒定自发光色用于标示面光源的位置和方向，不代表其在受光表面上的积分结果。


out vec4 color;
uniform vec3 lightColor;

void main()
{
	color = vec4(lightColor, 1.0f);
}
