#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：材质练习的 Phong 片段着色器；结构与上一节相同，便于只比较 uniform 参数。
// 输入输出：Material 和 Light 的各分量由 CPU 设置，世界空间位置、法线由顶点阶段插值。
// 本练习新增：用预设青色塑料的 ambient/diffuse/specular/shininess 观察材质外观。
out vec4 FragColor;

struct Material {
    // 同一几何体可仅替换这些系数，模拟不同表面而无需改动光照公式。
    vec3 ambient;
    vec3 diffuse;
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
  
uniform vec3 viewPos;
uniform Material material;
uniform Light light;

void main()
{
    // ambient
    vec3 ambient = light.ambient * material.ambient;
  	
    // diffuse 
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * (diff * material.diffuse);
    
    // specular
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);  
    // 材质高光指数控制反射瓣宽度，specular 颜色控制高光反射率。
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * (spec * material.specular);  
        
    // 三类材质响应独立计算后在线性色彩值中相加。
    vec3 result = ambient + diffuse + specular;
    FragColor = vec4(result, 1.0);
} 