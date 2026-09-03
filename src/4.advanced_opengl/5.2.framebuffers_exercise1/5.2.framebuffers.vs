#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：场景顶点着色器，被镜像离屏 Pass 与正常窗口 Pass 共同使用。
// 输入输出：局部位置经各 Pass 提供的 model/view/projection 变换，TexCoords 传递给纹理采样阶段。
// 数据关系：两次绘制使用相同几何和 Shader，仅 view 与当前绑定的 Framebuffer 不同。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    TexCoords = aTexCoords;    
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}