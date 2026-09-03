#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：背景天空盒片段着色器，按三维方向读取环境 Cubemap。
// 输入输出：TexCoords 为插值方向，skybox 与反射物体 Pass 共享同一纹理对象。
// 数据关系：此 Pass 写默认颜色附件的背景区域，视觉环境与物体反射来源保持一致。
out vec4 FragColor;

in vec3 TexCoords;

uniform samplerCube skybox;

void main()
{    
    FragColor = texture(skybox, TexCoords);
}