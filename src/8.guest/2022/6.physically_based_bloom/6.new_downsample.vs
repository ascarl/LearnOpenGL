#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：渐进式 Bloom 降采样的全屏顶点着色器，每个 mip 四边形顶点执行一次。
// 输入输出：读取二维 NDC 位置和 UV，输出裁剪空间位置与 texCoord；视口由 CPU 切换为当前 mip 尺寸。
// 数据流：几何保持不变，CPU 逐级把上一纹理绑定为输入、下一 mip 绑定为渲染目标。


layout (location = 0) in vec2 aPosition;
layout (location = 1) in vec2 aTexCoord;

out vec2 texCoord;

void main()
{
	gl_Position = vec4(aPosition.x, aPosition.y, 0.0, 1.0);
	texCoord = aTexCoord;
}
