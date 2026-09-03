// LearnOpenGL 中文导读
// 学习目标：把 Phong 光照移到顶点阶段形成 Gouraud 着色，并与逐片段计算的视觉质量比较。
// 核心流程：顶点 Shader 计算 LightingColor，光栅化器在三角形内部插值，片段 Shader 只乘 objectColor 后输出。
// 本练习新增：减少逐片段计算成本，但稀疏顶点无法捕获狭窄高光，插值还可能在三角形边界形成条带。
// 观察重点：本文件是两阶段 Shader 与结果说明的参考答案，不是可直接构建的 C++ 程序。
// Vertex shader:
// ================
#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;

out vec3 LightingColor; // resulting color from lighting calculations

uniform vec3 lightPos;
uniform vec3 viewPos;
uniform vec3 lightColor;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    gl_Position = projection * view * model * vec4(aPos, 1.0);
    
    // gouraud shading
    // ------------------------
    // 与逐片段 Phong 不同，下面整套光照公式每个顶点只执行一次。
    vec3 Position = vec3(model * vec4(aPos, 1.0));
    vec3 Normal = mat3(transpose(inverse(model))) * aNormal;
    
    // ambient
    float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * lightColor;
  	
    // diffuse 
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - Position);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * lightColor;
    
    // specular
    float specularStrength = 1.0; // this is set higher to better show the effect of Gouraud shading 
    vec3 viewDir = normalize(viewPos - Position);
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
    vec3 specular = specularStrength * spec * lightColor;      

    // 光栅化器会在三角形内部插值这个顶点颜色，狭窄高光可能在顶点间被漏掉或失真。
    LightingColor = ambient + diffuse + specular;
}


// Fragment shader:
// ================
#version 330 core
out vec4 FragColor;

in vec3 LightingColor; 

uniform vec3 objectColor;

void main()
{
   // 片段阶段不再重算法线和方向，只消费插值后的 LightingColor。
   FragColor = vec4(LightingColor * objectColor, 1.0);
}


/*
So what do we see?
You can see (for yourself or in the provided image) the clear distinction of the two triangles at the front of the 
cube. This 'stripe' is visible because of fragment interpolation. From the example image we can see that the top-right 
vertex of the cube's front face is lit with specular highlights. Since the top-right vertex of the bottom-right triangle is 
lit and the other 2 vertices of the triangle are not, the bright values interpolates to the other 2 vertices. The same 
happens for the upper-left triangle. Since the intermediate fragment colors are not directly from the light source 
but are the result of interpolation, the lighting is incorrect at the intermediate fragments and the top-left and 
bottom-right triangle collide in their brightness resulting in a visible stripe between both triangles.

This effect will become more apparent when using more complicated shapes.
*/