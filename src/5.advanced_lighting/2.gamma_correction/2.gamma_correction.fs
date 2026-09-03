#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器，对四个点光源累加 Blinn-Phong 漫反射与镜面高光，并对 gamma 路径采用平方反比衰减和显示编码。
// 输入输出：floorTexture 在启用路径中由 GL_SRGB 自动解码；lightPositions/lightColors 与 FragPos/Normal 均为世界空间数据。
// 核心算法：线性光照采用 1/d² 衰减，最终 pow(color,1/2.2) 是显示编码；不能在 sRGB 数值上直接做光照积分。
out vec4 FragColor;

in VS_OUT {
    vec3 FragPos;
    vec3 Normal;
    vec2 TexCoords;
} fs_in;

uniform sampler2D floorTexture;

uniform vec3 lightPositions[4];
uniform vec3 lightColors[4];
uniform vec3 viewPos;
uniform bool gamma;

vec3 BlinnPhong(vec3 normal, vec3 fragPos, vec3 lightPos, vec3 lightColor)
{
    // diffuse
    vec3 lightDir = normalize(lightPos - fragPos);
    float diff = max(dot(lightDir, normal), 0.0);
    vec3 diffuse = diff * lightColor;
    // specular
    vec3 viewDir = normalize(viewPos - fragPos);
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = 0.0;
    vec3 halfwayDir = normalize(lightDir + viewDir);  
    spec = pow(max(dot(normal, halfwayDir), 0.0), 64.0);
    vec3 specular = spec * lightColor;    
    // simple attenuation
    float max_distance = 1.5;
    float distance = length(lightPos - fragPos);
    // 在线性辐射度空间中，理想点光源能量随球面面积增长而按 1/d² 衰减。
    float attenuation = 1.0 / (gamma ? distance * distance : distance);
    
    diffuse *= attenuation;
    specular *= attenuation;
    
    return diffuse + specular;
}

void main()
{           
    vec3 color = texture(floorTexture, fs_in.TexCoords).rgb;
    vec3 lighting = vec3(0.0);
    for(int i = 0; i < 4; ++i)
        lighting += BlinnPhong(normalize(fs_in.Normal), fs_in.FragPos, lightPositions[i], lightColors[i]);
    color *= lighting;
    if(gamma)
        // 光照完成后才编码到近似 sRGB；该幂不是额外光照，而是面向显示设备的传输变换。
        color = pow(color, vec3(1.0/2.2));
    FragColor = vec4(color, 1.0);
}