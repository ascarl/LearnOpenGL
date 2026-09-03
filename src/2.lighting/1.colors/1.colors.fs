#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：物体片段着色器；向默认颜色附件输出不透明 RGB 颜色。
// 输入输出：objectColor 与 lightColor 是 CPU 设置的 uniform，本示例还没有法线和光照方向。
// 核心算法：按通道相乘模拟有色光对物体固有颜色的筛选，为完整光照模型做直观铺垫。
out vec4 FragColor;
  
uniform vec3 objectColor;
uniform vec3 lightColor;

void main()
{
    // 两种颜色逐通道相乘；任一通道为零都会滤掉该通道的光。
    FragColor = vec4(lightColor * objectColor, 1.0);
}