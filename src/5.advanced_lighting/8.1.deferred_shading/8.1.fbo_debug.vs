// vertex shader
#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：G-buffer 调试四边形顶点着色器，直接输出屏幕位置和附件采样 UV。
// 输入输出：position 是二维裁剪空间坐标，TexCoords 供调试片段阶段使用。
// Pass 依赖：用于把任意 FBO 颜色附件显示到屏幕，不参与正式延迟光照。
layout (location = 0) in vec2 position;
layout (location = 1) in vec2 texCoords;

out vec2 TexCoords;

void main()
{
    gl_Position = vec4(position, 0.0f, 1.0f);
    TexCoords = texCoords;
}
 