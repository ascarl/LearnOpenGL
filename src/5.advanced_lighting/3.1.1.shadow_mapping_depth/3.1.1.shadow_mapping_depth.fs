#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：方向光深度 Pass 的片段着色器，不写颜色附件。
// 输入输出：固定功能深度测试自动把 gl_FragCoord.z 写入 FBO 的 depthMap。
// Pass 依赖：该文件刻意为空；真正产物是光空间最近表面深度，而不是可见颜色。

void main()
{             
    // gl_FragDepth = gl_FragCoord.z;
}