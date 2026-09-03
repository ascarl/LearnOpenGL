#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器；对局部位置应用 CPU 上传的二维组合变换。
// 输入输出：aPos 与 aTexCoord 来自 VAO，uniform mat4 transform 生成 gl_Position，TexCoord 直传片段阶段。
// 矩阵顺序：列向量位于右侧，transform * position 会从组合矩阵最右侧的变换开始生效。

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoord;

out vec2 TexCoord;

uniform mat4 transform;

void main()
{
	gl_Position = transform * vec4(aPos, 1.0);
	TexCoord = vec2(aTexCoord.x, aTexCoord.y);
}