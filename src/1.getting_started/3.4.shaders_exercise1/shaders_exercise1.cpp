// LearnOpenGL 中文导读
// 文件性质：这是 3.3 顶点着色器的练习答案片段，不是独立 C++ 程序。
// 与基础示例的精确差异：仅把裁剪空间位置的 y 分量改为 -aPos.y，使三角形沿 x 轴上下翻转；颜色传递保持不变。
// 观察重点：符号翻转发生在顶点阶段，光栅化后的整幅几何随之翻转。

#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aColor;

out vec3 ourColor;

void main()
{
    // 关键步骤：只取反 y，不改变 x/z/w；所有顶点都做同样处理，因此图元整体镜像。
    gl_Position = vec4(aPos.x, -aPos.y, aPos.z, 1.0); // just add a - to the y position
    ourColor = aColor;
}