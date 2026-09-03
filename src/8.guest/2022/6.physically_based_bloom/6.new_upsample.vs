#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：渐进式 Bloom 上采样的全屏顶点着色器，每个目标 mip 四边形顶点执行一次。
// 输入输出：读取二维 NDC 位置和 UV，输出裁剪空间位置与 texCoord；CPU 从小 mip 向大 mip 反向迭代。
// 数据流：片段结果通过 GL_ONE/GL_ONE 加法混合写入已经含有较高频内容的下一层。


layout (location = 0) in vec2 aPosition;
layout (location = 1) in vec2 aTexCoord;

out vec2 texCoord;

void main()
{
	gl_Position = vec4(aPosition.x, aPosition.y, 0.0, 1.0);
	texCoord = aTexCoord;
}
