#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：光照贴图综合练习的片段着色器，额外叠加不依赖入射光的自发光纹理。
// 输入输出：diffuse/specular/emission 三个采样器共享 TexCoords，输出写入默认颜色附件。
// 本练习新增：emission 直接加入 Phong 三项结果，使指定纹素即使未被照亮也保持发光颜色。
out vec4 FragColor;

struct Material {
    sampler2D diffuse;
    sampler2D specular;    
    // emission 标记材质自身发出的颜色，不代表场景中会照亮其他物体的真实光源。
    sampler2D emission;
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
    vec3 specular = light.specular * spec * texture(material.specular, TexCoords).rgb;  
    
      // emission
    // 自发光项没有法线、光源方向或衰减因子，直接由纹理决定。
    vec3 emission = texture(material.emission, TexCoords).rgb;
        
    // 自发光与反射光在最终输出前相加。
    vec3 result = ambient + diffuse + specular + emission;
    FragColor = vec4(result, 1.0);
} 