#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：屏幕 Pass 的片段着色器，读取第一 Pass 的颜色附件并原样显示。
// 输入输出：screenTexture 是离屏 GL_COLOR_ATTACHMENT0；FragColor 写入默认帧缓冲颜色附件。
// 扩展思路：在这里对 col 做卷积、灰度或反相，可在不重绘场景的情况下实现屏幕后处理。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D screenTexture;

void main()
{
    vec3 col = texture(screenTexture, TexCoords).rgb;
    FragColor = vec4(col, 1.0);
} 