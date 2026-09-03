#version 410 core
// LearnOpenGL 中文导读
// 着色阶段：CSM 深度 Pass 的空片段着色器，每个生成片元执行一次但不写颜色。
// 输入输出：没有用户输入输出；固定管线自动把光空间深度写入几何着色器选定的 depthMap 数组层。
// 核心算法：FBO 只有深度附件，因此无需显式计算颜色或 gl_FragDepth。


void main()
{             
}
