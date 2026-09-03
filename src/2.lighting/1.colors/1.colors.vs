#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：物体顶点着色器；把局部空间位置依次变换到世界、观察和裁剪空间。
// 输入输出：aPos 是位置属性，model/view/projection 由 CPU 上传；本阶段没有额外的跨阶段输出。
// 本节新增：物体和灯使用独立 Shader，为后续把表面属性与光源属性分开计算建立基础。
layout (location = 0) in vec3 aPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
	// OpenGL 按从右到左的顺序应用 model、view、projection，最终写入裁剪空间位置。
	gl_Position = projection * view * model * vec4(aPos, 1.0);
}