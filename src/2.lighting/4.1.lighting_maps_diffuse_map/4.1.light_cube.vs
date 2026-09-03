#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：漫反射贴图示例中灯标记的顶点着色器。
// 输入输出：只需要位置与 MVP uniform；灯标记不读取主物体的法线和纹理坐标。
// 观察重点：复用同一 VBO 时，独立 VAO 可只启用位置属性。
layout (location = 0) in vec3 aPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    // 标准 MVP 变换把灯标记放入裁剪空间。
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}