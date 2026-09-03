#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：法线贴图顶点着色器，构造世界空间正交 TBN 后转置为 world-to-tangent 变换。
// 输入输出：aTangent/aNormal 经 normalMatrix 变换并 Gram-Schmidt 正交化；输出切线空间光源、相机和片元位置。
// 核心算法：把所有光照向量移到切线空间，可直接与法线纹理中编码的局部微表面方向点乘。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;
layout (location = 3) in vec3 aTangent;
layout (location = 4) in vec3 aBitangent;

out VS_OUT {
    vec3 FragPos;
    vec2 TexCoords;
    vec3 TangentLightPos;
    vec3 TangentViewPos;
    vec3 TangentFragPos;
} vs_out;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

uniform vec3 lightPos;
uniform vec3 viewPos;

void main()
{
    vs_out.FragPos = vec3(model * vec4(aPos, 1.0));   
    vs_out.TexCoords = aTexCoords;
    
    mat3 normalMatrix = transpose(inverse(mat3(model)));
    vec3 T = normalize(normalMatrix * aTangent);
    vec3 N = normalize(normalMatrix * aNormal);
    // Gram-Schmidt 去掉 T 在 N 上的分量，避免插值或模型变换后基向量不再正交。
    T = normalize(T - dot(T, N) * N);
    vec3 B = cross(N, T);
    
    // mat3(T,B,N) 是 tangent-to-world；正交基的逆等于转置，因此这里得到 world-to-tangent。
    mat3 TBN = transpose(mat3(T, B, N));    
    vs_out.TangentLightPos = TBN * lightPos;
    vs_out.TangentViewPos  = TBN * viewPos;
    vs_out.TangentFragPos  = TBN * vs_out.FragPos;
        
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}