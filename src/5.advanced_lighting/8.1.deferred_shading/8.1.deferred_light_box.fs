#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：灯箱可视化片段着色器，直接输出对应点光源颜色。
// 输入输出：lightColor 是 CPU 生成的灯光颜色，FragColor 写默认帧缓冲。
// Pass 依赖：这是延迟光照后的前向覆盖 Pass，依靠已复制的深度而不是 G-buffer 颜色附件。
layout (location = 0) out vec4 FragColor;

uniform vec3 lightColor;

void main()
{           
    FragColor = vec4(lightColor, 1.0);
}