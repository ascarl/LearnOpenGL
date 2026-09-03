#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器；消费鼠标朝向和滚轮 FOV 所生成的 View/Projection。
// 输入输出：MVP 把局部顶点送入裁剪空间，TexCoord 传给片段阶段。
// 坐标空间：yaw/pitch/zoom 在 CPU 侧转换为矩阵，Shader 无需知道输入设备或欧拉角。

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoord;

out vec2 TexCoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
	gl_Position = projection * view * model * vec4(aPos, 1.0f);
	TexCoord = vec2(aTexCoord.x, aTexCoord.y);
}