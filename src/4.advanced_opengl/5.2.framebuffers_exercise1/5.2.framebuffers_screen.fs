#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：镜面叠加 Pass 的片段着色器，读取第一 Pass 生成的反向相机画面。
// 输入输出：screenTexture 指向离屏颜色附件，FragColor 把采样结果写入默认帧缓冲的小四边形区域。
// 数据关系：该 Pass 不读取离屏深度/模板 Renderbuffer，只消费可采样的颜色纹理。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D screenTexture;

void main()
{
    vec3 col = texture(screenTexture, TexCoords).rgb;
    FragColor = vec4(col, 1.0);
} 