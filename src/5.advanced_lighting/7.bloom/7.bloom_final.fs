#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：Bloom 最终合成片段着色器，把原始 HDR 场景与模糊高亮相加后输出 LDR。
// 输入输出：scene 读取 MRT 场景附件，bloomBlur 读取最后一次 ping-pong 结果，bloom 控制是否叠加。
// 核心算法：线性 HDR 中加法形成光晕，随后指数曝光映射压缩范围，最后执行 1/2.2 显示编码。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D scene;
uniform sampler2D bloomBlur;
uniform bool bloom;
uniform float exposure;

void main()
{             
    const float gamma = 2.2;
    vec3 hdrColor = texture(scene, TexCoords).rgb;      
    vec3 bloomColor = texture(bloomBlur, TexCoords).rgb;
    if(bloom)
        // 两者仍是线性 HDR 辐亮度，先相加才保持能量意义；显示编码后的颜色不能这样合成。
        hdrColor += bloomColor; // additive blending
    // tone mapping
    vec3 result = vec3(1.0) - exp(-hdrColor * exposure);
    // also gamma correct while we're at it       
    result = pow(result, vec3(1.0 / gamma));
    FragColor = vec4(result, 1.0);
}