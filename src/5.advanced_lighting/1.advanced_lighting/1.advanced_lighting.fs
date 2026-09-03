#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器，在世界空间计算环境光、漫反射和可切换的镜面反射。
// 输入输出：读取地板纹理以及 FragPos/Normal，uniform 提供 lightPos、viewPos 与 blinn 开关。
// 核心算法：Phong 使用反射向量 R，Blinn-Phong 使用 H=normalize(L+V)；N·H 表示法线与微表面半程方向的一致程度。
out vec4 FragColor;

in VS_OUT {
    vec3 FragPos;
    vec3 Normal;
    vec2 TexCoords;
} fs_in;

uniform sampler2D floorTexture;
uniform vec3 lightPos;
uniform vec3 viewPos;
uniform bool blinn;

void main()
{           
    vec3 color = texture(floorTexture, fs_in.TexCoords).rgb;
    // ambient
    vec3 ambient = 0.05 * color;
    // diffuse
    vec3 lightDir = normalize(lightPos - fs_in.FragPos);
    vec3 normal = normalize(fs_in.Normal);
    float diff = max(dot(lightDir, normal), 0.0);
    vec3 diffuse = diff * color;
    // specular
    vec3 viewDir = normalize(viewPos - fs_in.FragPos);
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = 0.0;
    if(blinn)
    {
        // H 是入射光 L 与观察方向 V 的角平分线；N·H 越大，法线越接近产生镜面反射的微表面方向。
        vec3 halfwayDir = normalize(lightDir + viewDir);  
        spec = pow(max(dot(normal, halfwayDir), 0.0), 32.0);
    }
    else
    {
        // Phong 直接比较观察方向 V 与理想反射方向 R，因此低掠射角可能出现高光截断。
        vec3 reflectDir = reflect(-lightDir, normal);
        spec = pow(max(dot(viewDir, reflectDir), 0.0), 8.0);
    }
    vec3 specular = vec3(0.3) * spec; // assuming bright white light color
    FragColor = vec4(ambient + diffuse + specular, 1.0);
}