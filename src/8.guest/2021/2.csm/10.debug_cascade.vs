#version 410 core
// LearnOpenGL 中文导读
// 着色阶段：级联视锥体积调试几何的顶点着色器，每个可视化网格顶点执行一次。
// 输入输出：aPos 已由 CPU 从逆光空间矩阵还原到世界空间；view/projection 把它投影到当前相机画面。
// 核心算法：不使用 model 矩阵，直接显示各级联在世界空间覆盖的包围体。

layout (location = 0) in vec3 aPos;

uniform mat4 view;
uniform mat4 projection;

void main()
{
    gl_Position = projection * view * vec4(aPos, 1.0);
}
