#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器；按插值 UV 从一张 2D 纹理取样。
// 输入输出：TexCoord 来自顶点阶段，sampler2D texture1 指向 CPU 绑定的纹理单元，FragColor 写入颜色附件。
// 核心算法：texture(texture1, TexCoord) 依据纹理的环绕、过滤和 mipmap 状态返回 RGBA。

out vec4 FragColor;

in vec3 ourColor;
in vec2 TexCoord;

// texture sampler
uniform sampler2D texture1;

void main()
{
	FragColor = texture(texture1, TexCoord);
}