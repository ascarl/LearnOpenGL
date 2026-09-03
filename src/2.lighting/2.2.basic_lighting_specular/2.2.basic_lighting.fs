#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：完整基础 Phong 光照的片段着色器，输出环境光、漫反射与镜面反射之和。
// 输入输出：世界空间 Normal/FragPos 与 uniform 光源、相机位置共同构造光线和视线方向。
// 本节新增：依据反射向量与观察方向的夹角计算镜面高光，指数 32 控制高光集中程度。
out vec4 FragColor;

in vec3 Normal;  
in vec3 FragPos;  
  
uniform vec3 lightPos; 
uniform vec3 viewPos; 
uniform vec3 lightColor;
uniform vec3 objectColor;

void main()
{
    // ambient
    float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * lightColor;
  	
    // diffuse 
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * lightColor;
    
    // specular
    float specularStrength = 0.5;
    vec3 viewDir = normalize(viewPos - FragPos);
    // reflect 的入射参数要指向表面，因此使用 -lightDir；结果是理想镜面反射方向。
    vec3 reflectDir = reflect(-lightDir, norm);  
    // 幂指数越大，只有越接近反射方向的视线才能得到明显高光。
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
    vec3 specular = specularStrength * spec * lightColor;  
        
    // 环境项不使用方向；只有漫反射和镜面反射涉及的向量需要并已统一在世界空间计算。
    vec3 result = (ambient + diffuse + specular) * objectColor;
    FragColor = vec4(result, 1.0);
} 