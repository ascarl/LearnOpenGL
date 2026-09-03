// fragment shader
#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：G-buffer 调试片段着色器，原样显示指定的帧缓冲附件。
// 输入输出：fboAttachment 可绑定 position、normal 或 albedo/spec 纹理，FragColor 直接写采样值。
// 阅读提示：法线和浮点位置若不先重映射，屏幕颜色只是原始数值的粗略诊断。
out vec4 FragColor;
in  vec2 TexCoords;
  
uniform sampler2D fboAttachment;
  
void main()
{
    FragColor = texture(fboAttachment, TexCoords);
} 