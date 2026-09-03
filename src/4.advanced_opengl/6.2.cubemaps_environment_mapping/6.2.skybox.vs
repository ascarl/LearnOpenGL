#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：环境映射示例的天空盒顶点着色器，输出 Cubemap 采样方向并固定到最远深度。
// 输入输出：aPos 同时充当方向；view 只保留相机旋转，projection 生成透视裁剪坐标。
// 核心算法：把 z 替换为 w 后 NDC 深度为 1，配合 GL_LEQUAL 只覆盖没有近景几何的位置。
layout (location = 0) in vec3 aPos;

out vec3 TexCoords;

uniform mat4 projection;
uniform mat4 view;

void main()
{
    TexCoords = aPos;
    vec4 pos = projection * view * vec4(aPos, 1.0);
    gl_Position = pos.xyww;
}  