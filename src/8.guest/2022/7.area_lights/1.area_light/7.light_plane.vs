#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：矩形发光面可视化的顶点着色器，每个光源平面顶点执行一次。
// 输入输出：读取局部位置并应用 model/view/projection，直接输出裁剪空间位置；法线和 UV 在此可视化 Pass 中未使用。
// 数据流：它只画出光源几何外观，真正的 LTC 光照由受光平面的另一套 Shader 计算。


layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexcoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
	gl_Position = projection * view * model * vec4(aPosition, 1.0f);
}
