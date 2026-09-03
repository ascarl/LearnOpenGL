#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：粒子顶点着色器，把共享单位四边形缩放后平移到每个 CPU 粒子的位置。
// 输入输出：vertex 包含位置/纹理坐标，offset/color 为逐粒子 uniform，TexCoords 与 ParticleColor 传给片段阶段。
// 坐标空间：固定 10 像素缩放和 offset 生成屏幕坐标，再由二维正交 projection 映射到裁剪空间。

layout (location = 0) in vec4 vertex; // <vec2 position, vec2 texCoords>

out vec2 TexCoords;
out vec4 ParticleColor;

uniform mat4 projection;
uniform vec2 offset;
uniform vec4 color;

void main()
{
    float scale = 10.0f;
    TexCoords = vertex.zw;
    ParticleColor = color;
    gl_Position = projection * vec4((vertex.xy * scale) + offset, 0.0, 1.0);
}