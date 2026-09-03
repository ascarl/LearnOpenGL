#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：天空盒片段着色器，按观察方向从六面 Cubemap 自动选择面并采样。
// 输入输出：三维 TexCoords 指向立方体表面方向，skybox 返回对应环境颜色并写入默认颜色附件。
// 观察重点：面选择和面内二维坐标由硬件根据方向分量完成，边缘使用 CLAMP_TO_EDGE 避免接缝。
out vec4 FragColor;

in vec3 TexCoords;

uniform samplerCube skybox;

void main()
{    
    FragColor = texture(skybox, TexCoords);
}