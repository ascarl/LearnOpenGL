#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：多面光源可视化顶点着色器，每个矩形实例的每个顶点分别执行一次。
// 输入输出：CPU 逐灯更新 model，把共享局部矩形变换到世界空间，再经 view/projection 输出裁剪位置。
// 数据流：该 Pass 只显示 16 个发光平面，不负责把光照贡献传给受光表面。


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
