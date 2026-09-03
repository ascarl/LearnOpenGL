#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：离屏场景 Pass 的片段着色器，输出普通二维纹理颜色。
// 输入输出：TexCoords 采样 texture1，FragColor 写入自定义 Framebuffer 的 GL_COLOR_ATTACHMENT0。
// 数据关系：这张颜色附件稍后作为 screenTexture 被屏幕 Pass 读取，实现渲染到纹理。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture1;

void main()
{    
    FragColor = texture(texture1, TexCoords);
}