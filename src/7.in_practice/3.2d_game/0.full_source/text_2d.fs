#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：游戏文字片段着色器，把单通道字形覆盖率与指定 RGB 组合。
// 输入输出：text 的红通道保存 FreeType 灰度位图，textColor 提供颜色，color 输出到默认帧缓冲。
// 核心算法：采样覆盖率作为 Alpha 并乘以文字颜色，配合 SRC_ALPHA 混合叠加在后处理画面之上。

in vec2 TexCoords;
out vec4 color;

uniform sampler2D text;
uniform vec3 textColor;

void main()
{    
    vec4 sampled = vec4(1.0, 1.0, 1.0, texture(text, TexCoords).r);
    color = vec4(textColor, 1.0) * sampled;
}  