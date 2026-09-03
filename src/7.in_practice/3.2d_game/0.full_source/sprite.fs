#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：精灵片段着色器，为纹理颜色施加每对象色调。
// 输入输出：TexCoords 为插值纹理坐标，sprite/spriteColor 由渲染器设置，color 写入当前场景颜色附件。
// 核心算法：纹理 RGBA 与 vec4(spriteColor, 1) 相乘；透明边缘依靠全局 Alpha 混合合成。

in vec2 TexCoords;
out vec4 color;

uniform sampler2D sprite;
uniform vec3 spriteColor;

void main()
{
    
    color = vec4(spriteColor, 1.0) * texture(sprite, TexCoords);
}