#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：点光源可视标记的片段着色器。
// 输入输出：无材质与距离计算，直接向默认颜色附件写入白色。
// 观察重点：标记位置对应点光源的 light.position，标记本身不受衰减。
out vec4 FragColor;

void main()
{
    // 使用恒定白色将光源位置从受光场景中区分出来。
    FragColor = vec4(1.0); // set all 4 vector values to 1.0
}