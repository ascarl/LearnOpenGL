#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：基础 Phong 光照的片段着色器，本节只计算环境光与 Lambert 漫反射。
// 输入输出：FragPos 已变换到世界空间；Normal 仍直接来自模型空间 aNormal，仅因当前 model 为单位矩阵才在数值上兼容。
// 学习阶段捷径：若 model 含旋转或非均匀缩放，必须用法线矩阵把 Normal 正确变换到世界空间。
// 本节新增：用法线与入射光方向的点积描述表面朝向，背光面通过 max 限制为零。
out vec4 FragColor;

in vec3 Normal;  
in vec3 FragPos;  
  
uniform vec3 lightPos; 
uniform vec3 lightColor;
uniform vec3 objectColor;

void main()
{
    // ambient
    // 环境光与方向无关，保证未被直接照亮的区域仍保留少量可见颜色。
    float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * lightColor;
  	
    // diffuse 
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);
    // 单位向量点积等于夹角余弦，表面越正对光源，漫反射越强。
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * lightColor;
            
    // 光照强度之和再乘物体固有色，得到该片段最终反射的 RGB。
    vec3 result = (ambient + diffuse) * objectColor;
    FragColor = vec4(result, 1.0);
} 