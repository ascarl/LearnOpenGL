#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：普通纹理几何顶点着色器，输出 UV 并执行相机 MVP 变换。
// 输入输出：position/texCoords 经 model、view、projection 进入裁剪空间；没有级联索引或光空间输出。
// 阅读提示：尽管目录名为 csm，本 Shader 并未实现级联阴影所需的分层深度或多光矩阵。
layout (location = 0) in vec3 position;
layout (location = 1) in vec2 texCoords;

out vec2 TexCoords;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    gl_Position = projection * view * model * vec4(position, 1.0f);
    TexCoords = texCoords;
}