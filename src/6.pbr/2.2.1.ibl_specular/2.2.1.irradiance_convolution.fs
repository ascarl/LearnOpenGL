#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：漫反射 IBL 预计算片段着色器，为每个法线方向积分环境半球辐照度。
// 输入输出：WorldPos 定义世界空间 N，environmentMap 提供入射 radiance，输出写低分辨率 irradianceMap 当前面。
// 核心算法：累加 L_i*cos(theta)*sin(theta)；cos 是 Lambert 投影权重，sin 是球坐标立体角雅可比，结果近似半球积分。
out vec4 FragColor;
in vec3 WorldPos;

uniform samplerCube environmentMap;

const float PI = 3.14159265359;

void main()
{		
    // 输出 Cubemap 的世界方向就是该 texel 所代表表面朝向 N，积分域是以 N 为轴的上半球。
    vec3 N = normalize(WorldPos);

    vec3 irradiance = vec3(0.0);   
    
    // tangent space calculation from origin point
    vec3 up    = vec3(0.0, 1.0, 0.0);
    vec3 right = normalize(cross(up, N));
    up         = normalize(cross(N, right));
       
    float sampleDelta = 0.025;
    float nrSamples = 0.0f;
    for(float phi = 0.0; phi < 2.0 * PI; phi += sampleDelta)
    {
        for(float theta = 0.0; theta < 0.5 * PI; theta += sampleDelta)
        {
            // spherical to cartesian (in tangent space)
            vec3 tangentSample = vec3(sin(theta) * cos(phi),  sin(theta) * sin(phi), cos(theta));
            // tangent space to world
            vec3 sampleVec = tangentSample.x * right + tangentSample.y * up + tangentSample.z * N; 

            // cos(theta) 是 Lambert 投影权重，sin(theta) 是球坐标面积元；乘积近似对应方向的立体角贡献。
            irradiance += texture(environmentMap, sampleVec).rgb * cos(theta) * sin(theta);
            nrSamples++;
        }
    }
    irradiance = PI * irradiance * (1.0 / float(nrSamples));
    
    FragColor = vec4(irradiance, 1.0);
}
