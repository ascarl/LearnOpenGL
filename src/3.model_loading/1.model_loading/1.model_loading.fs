#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：模型加载示例的最小纹理片段着色器，直接显示导入材质的第一张漫反射纹理。
// 输入输出：TexCoords 来自 Mesh 顶点；Mesh::Draw 按命名约定把纹理绑定为 texture_diffuse1。
// 本章新增：Model 遍历节点并加载/缓存纹理对象；Mesh 创建缓冲、保存纹理句柄并在 Draw 时绑定，Shader 再按采样器约定取样。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture_diffuse1;

void main()
{    
    // 本示例聚焦导入链路，尚未加入法线、光源和多张材质纹理的光照组合。
    FragColor = texture(texture_diffuse1, TexCoords);
}