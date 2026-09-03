#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：方向光章节保留的灯标记片段着色器模板。
// 输入输出：无输入，固定向默认颜色附件写入纯白色。
// 观察重点：方向光没有可绘制的位置，当前主程序不会使用该程序渲染灯立方体。
out vec4 FragColor;

void main()
{
    // 若单独调用，所有灯标记片段均为白色。
    FragColor = vec4(1.0); // set all 4 vector values to 1.0
}