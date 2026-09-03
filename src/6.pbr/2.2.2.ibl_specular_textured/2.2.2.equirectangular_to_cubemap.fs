#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：环境转换片段着色器，把三维世界方向映射到二维等距柱状 HDR 纹理。
// 输入输出：WorldPos 归一化为方向，atan/asin 生成 [0,1] UV，FragColor 写当前 envCubemap 面。
// 核心算法：经度对应 atan(z,x)，纬度对应 asin(y)；六次捕获共同建立无方向歧义的浮点 Cubemap。
out vec4 FragColor;
in vec3 WorldPos;

uniform sampler2D equirectangularMap;

const vec2 invAtan = vec2(0.1591, 0.3183);
vec2 SampleSphericalMap(vec3 v)
{
    vec2 uv = vec2(atan(v.z, v.x), asin(v.y));
    uv *= invAtan;
    uv += 0.5;
    return uv;
}

void main()
{		
    vec2 uv = SampleSphericalMap(normalize(WorldPos));
    vec3 color = texture(equirectangularMap, uv).rgb;
    
    FragColor = vec4(color, 1.0);
}
