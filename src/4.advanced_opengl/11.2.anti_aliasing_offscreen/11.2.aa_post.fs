#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：最终屏幕后处理片段着色器，读取 resolve 后的单采样颜色并转换为灰度。
// 输入输出：screenTexture 来自 intermediateFBO 的 GL_COLOR_ATTACHMENT0，FragColor 写默认颜色附件。
// 核心算法：按 0.2126/0.7152/0.0722 加权 RGB 近似感知亮度，再复制到三个颜色通道。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D screenTexture;

void main()
{
    vec3 col = texture(screenTexture, TexCoords).rgb;
    // 人眼对绿色最敏感、蓝色最不敏感，因此不能简单取三个通道的算术平均值。
    float grayscale = 0.2126 * col.r + 0.7152 * col.g + 0.0722 * col.b;
    FragColor = vec4(vec3(grayscale), 1.0);
} 