#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：模型加载示例的顶点着色器；把 Assimp/Mesh 上传的位置变换到裁剪空间。
// 输入输出：位置、法线、UV 遵循 Mesh 的属性 0/1/2 约定；本示例只把 TexCoords 传给片段阶段。
// 本章新增：顶点不再由入口文件手写，Model 导入多个 Mesh 后以统一布局驱动同一个 Shader。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;

out vec2 TexCoords;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    // 法线属性在该最小纹理展示 Shader 中暂未使用，UV 则连接到材质 diffuse 纹理。
    TexCoords = aTexCoords;    
    // 每个 Mesh 共享场景级 model/view/projection，最终进入同一个裁剪空间。
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}