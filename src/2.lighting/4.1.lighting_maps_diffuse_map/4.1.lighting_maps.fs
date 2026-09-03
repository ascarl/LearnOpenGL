#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：使用漫反射贴图的 Phong 片段着色器，向默认颜色附件输出材质受光结果。
// 输入输出：TexCoords 定位 diffuse 纹理采样，世界空间 FragPos/Normal 负责方向计算。
// 本节新增：环境项与漫反射项都从同一漫反射贴图取表面颜色，镜面颜色仍是常量。
out vec4 FragColor;

struct Material {
    // sampler2D 保存纹理单元索引；真正的像素颜色由 TexCoords 在运行时采样。
    sampler2D diffuse;
    vec3 specular;    
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
    // 环境光也调制漫反射贴图，使暗处仍保留纹理图案而不是统一色块。
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
    // 此阶段还没有镜面贴图，整个表面共享同一个 material.specular。
    vec3 specular = light.specular * (spec * material.specular);  
        
    vec3 result = ambient + diffuse + specular;
    FragColor = vec4(result, 1.0);
} 