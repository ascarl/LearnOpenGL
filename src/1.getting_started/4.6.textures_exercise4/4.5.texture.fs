#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器；按运行时权重混合两个纹理采样。
// 与基础示例的精确差异：用 CPU 每帧上传的 uniform float mixValue 取代 4.2 中固定的 0.2。
// 核心算法：mix(texture1,texture2,mixValue) 在 0 时只显示容器、在 1 时只显示笑脸。

out vec4 FragColor;

in vec3 ourColor;
in vec2 TexCoord;

uniform float mixValue;

// texture samplers
uniform sampler2D texture1;
uniform sampler2D texture2;

void main()
{
	// linearly interpolate between both textures
	FragColor = mix(texture(texture1, TexCoord), texture(texture2, TexCoord), mixValue);
}