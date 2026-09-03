#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：镜面反射示例中灯标记的片段着色器。
// 输入输出：无纹理和 uniform，直接向默认颜色附件输出纯白色。
// 观察重点：该几何体只标识光源位置，不受环境、漫反射或镜面项影响。
out vec4 FragColor;

void main()
{
    // 固定白色便于把灯标记和受材质影响的物体区分开。
    FragColor = vec4(1.0); // set all 4 vector values to 1.0
}