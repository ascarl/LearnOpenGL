#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：HDR 后处理全屏顶点着色器，直接输出裁剪空间四边形并透传 UV。
// 输入输出：不使用模型或相机矩阵，TexCoords 用于读取场景 Pass 的 hdrBuffer。
// Pass 依赖：只在 RGBA16F 场景附件完成后运行。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

void main()
{
    TexCoords = aTexCoords;
    gl_Position = vec4(aPos, 1.0);
}