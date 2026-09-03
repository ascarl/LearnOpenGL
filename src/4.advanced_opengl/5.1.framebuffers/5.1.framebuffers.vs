#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：离屏场景 Pass 的顶点着色器，将立方体和地面从局部空间变换到裁剪空间。
// 输入输出：位置/纹理坐标来自 VAO，TexCoords 传给片段阶段，MVP uniform 由相机与当前模型提供。
// 渲染目标：光栅化片段写入自定义 Framebuffer 的颜色纹理和组合 Renderbuffer 的深度部分。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    TexCoords = aTexCoords;    
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}