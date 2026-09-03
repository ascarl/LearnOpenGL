#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器，用纹理 alpha 作为硬阈值裁剪植被背景。
// 输入输出：采样 texture1 得到 RGBA；alpha 小于 0.1 的片段被丢弃，其余颜色写入默认颜色附件。
// 观察重点：discard 会阻止该片段继续参与后续颜色与深度写入，因此透明背景不会遮挡后方物体。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture1;

void main()
{             
    vec4 texColor = texture(texture1, TexCoords);
    // 这是二值透明裁剪，不会产生玻璃式的半透明混色。
    if(texColor.a < 0.1)
        discard;
    FragColor = texColor;
}