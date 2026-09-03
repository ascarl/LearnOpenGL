#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：方向光的 Phong 片段着色器，输出贴图材质的环境、漫反射和镜面分量。
// 输入输出：世界空间 Normal/FragPos、TexCoords 和相机位置参与计算，Light 只提供 direction 而非 position。
// 本节新增：所有片段共享同一入射方向，不计算到光源的距离或衰减，适合模拟遥远太阳光。
out vec4 FragColor;

struct Material {
    sampler2D diffuse;
    sampler2D specular;    
    float shininess;
}; 

struct Light {
    //vec3 position;
    // direction 表示光线传播方向，构造“片段指向光源”的 lightDir 时需要取反。
    vec3 direction;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

in vec3 FragPos;  
in vec3 Normal;  
in vec2 TexCoords;
  
uniform vec3 viewPos;
uniform Material material;
uniform Light light;

void main()
{
    // ambient
    vec3 ambient = light.ambient * texture(material.diffuse, TexCoords).rgb;
  	
    // diffuse 
    vec3 norm = normalize(Normal);
    // vec3 lightDir = normalize(light.position - FragPos);
    // 方向光的入射方向不依赖 FragPos，且不做距离衰减；viewDir 仍由 FragPos 决定，因此镜面项会随观察关系变化。
    vec3 lightDir = normalize(-light.direction);  
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * diff * texture(material.diffuse, TexCoords).rgb;  
    
    // specular
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * spec * texture(material.specular, TexCoords).rgb;  
        
    // 无衰减因子，三项在整个场景保持相同的光源强度。
    vec3 result = ambient + diffuse + specular;
    FragColor = vec4(result, 1.0);
} 