#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：多光源示例中四个点光源标记的顶点着色器。
// 输入输出：每次绘制上传不同 model 矩阵，共用 view/projection，把同一立方体放到四个光源位置。
// 观察重点：方向光与相机聚光灯不绘制标记，只有具备固定位置的点光源使用该程序。
layout (location = 0) in vec3 aPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    // 相同 VAO 通过不同 model uniform 被重复绘制为四个灯立方体。
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}