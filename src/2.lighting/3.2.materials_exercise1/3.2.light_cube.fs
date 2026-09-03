#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：材质练习的灯标记片段着色器。
// 输入输出：不接收插值数据，直接写入不透明白色到默认颜色附件。
// 观察重点：固定灯色与变化的材质外观形成对照。
out vec4 FragColor;

void main()
{
    // 灯标记无需执行表面光照模型。
    FragColor = vec4(1.0); // set all 4 vector values to 1.0
}