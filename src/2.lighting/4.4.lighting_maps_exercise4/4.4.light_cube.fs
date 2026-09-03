#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：自发光贴图练习的灯标记片段着色器。
// 输入输出：无纹理或光照输入，直接输出白色至默认颜色附件。
// 观察重点：该输出与主物体 emission 纹理的自发光效果相互独立。
out vec4 FragColor;

void main()
{
    // 纯白灯标记只用于场景定位。
    FragColor = vec4(1.0); // set all 4 vector values to 1.0
}