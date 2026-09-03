#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：硬边聚光灯示例的顶点着色器；输出世界空间位置、法线和纹理坐标。
// 输入输出：片段阶段将 FragPos 与相机位置/方向比较，判断片段是否位于手电筒光锥内。
// 本节新增：点光源位置和方向共同形成有限角度的聚光区域，顶点数据布局不变。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;

out vec3 FragPos;
out vec3 Normal;
out vec2 TexCoords;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    FragPos = vec3(model * vec4(aPos, 1.0));
    // 所有聚光方向运算都在世界空间进行，因此法线也必须进入世界空间。
    Normal = mat3(transpose(inverse(model))) * aNormal;  
    TexCoords = aTexCoords;
    
    gl_Position = projection * view * vec4(FragPos, 1.0);
}