#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：顶点着色器；建立局部空间到世界、观察、裁剪空间的完整变换链。
// 输入输出：model/view/projection 为 CPU 上传的 uniform，TexCoord 直接传给片段阶段。
// MVP 顺序：projection*view*model*position 从右向左依次执行模型、观察、投影变换。

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoord;

out vec2 TexCoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
	gl_Position = projection * view * model * vec4(aPos, 1.0);
	TexCoord = vec2(aTexCoord.x, aTexCoord.y);
}