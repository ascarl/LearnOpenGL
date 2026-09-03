#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：HDR 场景光照与亮区提取片段着色器，每个可见场景片元执行一次。
// 输入输出：读取世界空间位置/法线、漫反射纹理、四个光源和相机位置；MRT 0 写完整 HDR 颜色，MRT 1 写阈值亮区。
// 核心算法：在线性空间累加平方反比衰减的漫反射光照，并用感知亮度权重判断是否进入 Bloom 输入附件。

layout (location = 0) out vec4 FragColor;
layout (location = 1) out vec4 BrightColor;

in VS_OUT {
    vec3 FragPos;
    vec3 Normal;
    vec2 TexCoords;
} fs_in;

struct Light {
    vec3 Position;
    vec3 Color;
};

uniform Light lights[4];
uniform sampler2D diffuseTexture;
uniform vec3 viewPos;

void main()
{           
    vec3 color = texture(diffuseTexture, fs_in.TexCoords).rgb;
    vec3 normal = normalize(fs_in.Normal);
    // ambient
    vec3 ambient = 0.0 * color;
    // lighting
    vec3 lighting = vec3(0.0);
    vec3 viewDir = normalize(viewPos - fs_in.FragPos);
    for(int i = 0; i < 4; i++)
    {
        // diffuse
        vec3 lightDir = normalize(lights[i].Position - fs_in.FragPos);
        float diff = max(dot(lightDir, normal), 0.0);
        vec3 result = lights[i].Color * diff * color;      
        // attenuation (use quadratic as we have gamma correction)
        float distance = length(fs_in.FragPos - lights[i].Position);
        result *= 1.0 / (distance * distance);
        lighting += result;
                
    }
    vec3 result = ambient + lighting;
    // check whether result is higher than some threshold, if so, output as bloom threshold color

    // Rec.709 亮度系数把 RGB 化为标量阈值；低亮度片元在 BrightColor 中写黑色但仍保留于 FragColor。
    float brightness = dot(result, vec3(0.2126, 0.7152, 0.0722));
    if(brightness > 1.0)
        BrightColor = vec4(result, 1.0);
    else
        BrightColor = vec4(0.0, 0.0, 0.0, 1.0);
    FragColor = vec4(result, 1.0);
}
