#version 430 core
// LearnOpenGL 中文导读
// 着色阶段：计算结果显示 Pass 的片段着色器，每个窗口片元执行一次。
// 输入输出：sampler2D tex 读取 Compute Shader 刚写完的 RGBA32F 纹理，输出 RGB 到默认颜色附件。
// 数据流：CPU 在 dispatch 后发出屏障再进入本次采样；屏障位应覆盖后续 texture fetch 的可见性要求。
// 平台要求：GLSL 4.30 / OpenGL 4.3；macOS 系统 OpenGL 最高 4.1，无法直接运行本示例。

out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D tex;

void main()
{             
    vec3 texCol = texture(tex, TexCoords).rgb;      
    FragColor = vec4(texCol, 1.0);
}
