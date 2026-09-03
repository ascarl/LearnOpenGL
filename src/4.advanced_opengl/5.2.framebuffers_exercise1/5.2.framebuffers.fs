#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：场景片段着色器，在镜像 Pass 或正常 Pass 中输出物体纹理颜色。
// 输入输出：texture1 提供立方体/地面纹理；输出位置由当前 Framebuffer 决定，而非 Shader 内部决定。
// 渲染目标：镜像 Pass 写离屏颜色附件，正常 Pass 写默认颜色附件，两者都使用各自的深度附件。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture1;

void main()
{    
    FragColor = texture(texture1, TexCoords);
}