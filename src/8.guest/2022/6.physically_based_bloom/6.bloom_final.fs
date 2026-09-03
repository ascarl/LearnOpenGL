#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：Bloom 最终合成、色调映射与 Gamma 校正片段着色器，每个窗口像素执行一次。
// 输入输出：读取完整 HDR scene、传统或多尺度 bloomBlur、exposure 和模式；输出默认帧缓冲可显示的 LDR RGBA。
// 核心算法：模式 2 加法叠加旧 Bloom，模式 3 按 bloomStrength 混合新 Bloom，随后应用指数曝光映射和 2.2 Gamma。

out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D scene;
uniform sampler2D bloomBlur;
uniform float exposure;
uniform float bloomStrength = 0.04f;
uniform int programChoice;

vec3 bloom_none()
{
    vec3 hdrColor = texture(scene, TexCoords).rgb;
    return hdrColor;
}

vec3 bloom_old()
{
    vec3 hdrColor = texture(scene, TexCoords).rgb;
    vec3 bloomColor = texture(bloomBlur, TexCoords).rgb;
    return hdrColor + bloomColor; // additive blending
}

vec3 bloom_new()
{
    vec3 hdrColor = texture(scene, TexCoords).rgb;
    vec3 bloomColor = texture(bloomBlur, TexCoords).rgb;
    return mix(hdrColor, bloomColor, bloomStrength); // linear interpolation
}

void main()
{
    // to bloom or not to bloom
    vec3 result = vec3(0.0);
    switch (programChoice)
    {
    case 1: result = bloom_none(); break;
    case 2: result = bloom_old(); break;
    case 3: result = bloom_new(); break;
    default:
        result = bloom_none(); break;
    }
    // tone mapping

    // 指数映射把无上限 HDR 压到 [0,1)，再做线性到近似 sRGB 的幂函数转换。
    result = vec3(1.0) - exp(-result * exposure);
    // also gamma correct while we're at it
    const float gamma = 2.2;
    result = pow(result, vec3(1.0 / gamma));
    FragColor = vec4(result, 1.0);
}
