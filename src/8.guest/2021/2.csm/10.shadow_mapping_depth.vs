#version 410 core
// LearnOpenGL 中文导读
// 着色阶段：CSM 分层深度 Pass 的顶点着色器，每个场景顶点执行一次。
// 输入输出：读取局部位置与 model，仅输出世界空间位置到 gl_Position，交给后续几何着色器继续处理。
// 核心算法：此处故意不乘光空间矩阵，因为同一顶点随后要分别投影到五个级联层。

layout (location = 0) in vec3 aPos;

uniform mat4 model;

void main()
{
    gl_Position = model * vec4(aPos, 1.0);
}
