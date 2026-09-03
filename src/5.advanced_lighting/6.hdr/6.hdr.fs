#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：HDR 后处理片段着色器，把线性浮点场景色映射为显示颜色。
// 输入输出：hdrBuffer 来自 RGBA16F 场景附件，exposure 控制指数曝光映射，hdr 开关用于比较。
// 核心算法：1-exp(-color*exposure) 压缩高动态范围，随后 1/2.2 幂仅负责线性到近似 sRGB 的显示编码。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D hdrBuffer;
uniform bool hdr;
uniform float exposure;

void main()
{             
    const float gamma = 2.2;
    vec3 hdrColor = texture(hdrBuffer, TexCoords).rgb;
    if(hdr)
    {
        // reinhard
        // vec3 result = hdrColor / (hdrColor + vec3(1.0));
        // exposure
        // 指数式来自曝光响应：输入辐亮度越高越趋近 1，但低亮区域仍近似线性保留层次。
        vec3 result = vec3(1.0) - exp(-hdrColor * exposure);
        // also gamma correct while we're at it       
        result = pow(result, vec3(1.0 / gamma));
        FragColor = vec4(result, 1.0);
    }
    else
    {
        vec3 result = pow(hdrColor, vec3(1.0 / gamma));
        FragColor = vec4(result, 1.0);
    }
}