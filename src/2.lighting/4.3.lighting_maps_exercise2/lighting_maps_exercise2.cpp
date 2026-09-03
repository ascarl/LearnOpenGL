#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：镜面贴图练习的片段 Shader 参考答案；本文件虽以 .cpp 命名，内容不是可独立编译的 C++。
// 输入输出：读取世界空间 FragPos/Normal 与 TexCoords，采样 diffuse/specular 后写入默认颜色附件。
// 本练习新增：对镜面贴图执行 1 - sample 反相，观察黑白区域交换后高光分布如何改变。
out vec4 FragColor;

struct Material {
    sampler2D diffuse;
    sampler2D specular;
    float     shininess;
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
    vec3 ambient = light.ambient * vec3(texture(material.diffuse, TexCoords));
  	
    // diffuse 
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * diff * vec3(texture(material.diffuse, TexCoords));  
    
    // specular
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    // 逐通道用 1 减去采样值：原本低反射的黑色区域变为高反射，白色区域反之。
    vec3 specular = light.specular * spec * (vec3(1.0) - vec3(texture(material.specular, TexCoords))); // here we inverse the sampled specular color. Black becomes white and white becomes black.
        
    FragColor = vec4(ambient + diffuse + specular, 1.0);  
} 