#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：材质示例中灯标记的片段着色器。
// 输入输出：不采样材质且没有 uniform，直接向默认颜色附件输出不透明白色。
// 观察重点：灯标记保持白色，只用于对照光源位置；动态 lightColor 仅上传给受光物体程序。
out vec4 FragColor;

void main()
{
    // 固定值使所有片段写入相同颜色。
    FragColor = vec4(1.0); // set all 4 vector values to 1.0
}