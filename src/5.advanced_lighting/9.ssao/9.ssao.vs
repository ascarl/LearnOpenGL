#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：SSAO 屏幕 Pass 顶点着色器，绘制全屏四边形并透传 UV。
// 输入输出：aPos 已在裁剪空间，TexCoords 对齐 gPosition、gNormal、noise 与遮蔽附件。
// Pass 依赖：同一顶点路径为 SSAO、模糊和最终光照的屏幕空间处理服务。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

void main()
{
    TexCoords = aTexCoords;
    gl_Position = vec4(aPos, 1.0);
}