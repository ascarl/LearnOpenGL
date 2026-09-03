#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：带漫反射与镜面贴图的 Phong 片段着色器。
// 输入输出：两个 sampler2D 分别绑定不同纹理单元，共享插值后的 TexCoords；结果写入默认颜色附件。
// 本节新增：镜面反射率按纹素变化，金属边框可产生高光，而木质区域可保持低反射。
out vec4 FragColor;

struct Material {
    sampler2D diffuse;
    // 镜面贴图通常以灰度表达反射强弱，也可以用 RGB 控制有色镜面反射。
    sampler2D specular;    
    float shininess;
}; 

struct Light {
    vec3 position;

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
    vec3 lightDir = normalize(light.position - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * diff * texture(material.diffuse, TexCoords).rgb;  
    
    // specular
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    // 角度项 spec 决定当前观察条件下的高光，贴图决定该表面位置能反射多少高光。
    vec3 specular = light.specular * spec * texture(material.specular, TexCoords).rgb;  
        
    vec3 result = ambient + diffuse + specular;
    FragColor = vec4(result, 1.0);
} 