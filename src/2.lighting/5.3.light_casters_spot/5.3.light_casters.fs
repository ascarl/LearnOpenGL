#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：相机手电筒的硬边聚光片段着色器。
// 输入输出：Light 同时携带位置、方向、截止余弦与衰减参数；材质由 diffuse/specular 贴图提供。
// 本节新增：比较光线与聚光轴夹角的余弦，光锥内计算直接光，光锥外只保留环境项。
out vec4 FragColor;

struct Material {
    sampler2D diffuse;
    sampler2D specular;    
    float shininess;
}; 

struct Light {
    vec3 position;  
    vec3 direction;
    // cutOff 存储截止角余弦；使用余弦可直接通过点积比较，避免逐片段调用反三角函数。
    float cutOff;
    float outerCutOff;
  
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
	
    float constant;
    float linear;
    float quadratic;
};

in vec3 FragPos;  
in vec3 Normal;  
in vec2 TexCoords;
  
uniform vec3 viewPos;
uniform Material material;
uniform Light light;

void main()
{
    vec3 lightDir = normalize(light.position - FragPos);
    
    // check if lighting is inside the spotlight cone
    // light.direction 从光源指向场景，-light.direction 则指回光源，并与锥内片段到光源的 lightDir 对齐。
    // 两个单位方向的点积给出相对聚光轴的夹角余弦，用于和 cutOff 比较。
    float theta = dot(lightDir, normalize(-light.direction)); 
    
    // 角度越小余弦越大，因此 theta 大于 cutOff 才落在光锥内部；该分支形成清晰硬边。
    if(theta > light.cutOff) // remember that we're working with angles as cosines instead of degrees so a '>' is used.
    {    
        // ambient
        vec3 ambient = light.ambient * texture(material.diffuse, TexCoords).rgb;
        
        // diffuse 
        vec3 norm = normalize(Normal);
        float diff = max(dot(norm, lightDir), 0.0);
        vec3 diffuse = light.diffuse * diff * texture(material.diffuse, TexCoords).rgb;  
        
        // specular
        vec3 viewDir = normalize(viewPos - FragPos);
        vec3 reflectDir = reflect(-lightDir, norm);  
        float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
        vec3 specular = light.specular * spec * texture(material.specular, TexCoords).rgb;  
        
        // attenuation
        float distance    = length(light.position - FragPos);
        float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));    

        // ambient  *= attenuation; // remove attenuation from ambient, as otherwise at large distances the light would be darker inside than outside the spotlight due the ambient term in the else branch
        diffuse   *= attenuation;
        specular *= attenuation;   
            
        vec3 result = ambient + diffuse + specular;
        FragColor = vec4(result, 1.0);
    }
    else 
    {
        // else, use ambient light so scene isn't completely dark outside the spotlight.
        // 光锥外不计算漫反射和镜面项，但保留环境色以维持场景轮廓。
        FragColor = vec4(light.ambient * texture(material.diffuse, TexCoords).rgb, 1.0);
    }
} 