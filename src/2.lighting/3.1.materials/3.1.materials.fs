#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：材质版 Phong 片段着色器；向默认颜色附件输出三类反射分量之和。
// 输入输出：Material 描述表面各分量和高光指数，Light 描述光源位置及各分量强度。
// 本节新增：把材质与光源属性逐项相乘，使同一束光在不同材质上呈现不同反射响应。
out vec4 FragColor;

struct Material {
    // ambient/diffuse/specular 分别控制表面对三类光照分量的反射颜色。
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;    
    float shininess;
}; 

struct Light {
    vec3 position;

    // 光源也为三类分量提供独立强度，CPU 可据此塑造环境氛围。
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
    // shininess 越大，高光越小而集中；材质 specular 决定可反射的高光颜色。
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * (spec * material.specular);  
        
    // 每一项已分别组合“光源强度 × 材质响应”，因此这里直接累加。
    vec3 result = ambient + diffuse + specular;
    FragColor = vec4(result, 1.0);
} 