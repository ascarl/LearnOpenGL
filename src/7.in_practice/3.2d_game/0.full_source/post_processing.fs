#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：后处理片段着色器，从已 resolve 的场景纹理生成最终窗口颜色。
// 输入输出：scene 是单采样颜色纹理，offsets 与两组 3x3 kernel 来自 CPU，color 写入默认帧缓冲。
// 核心算法：chaos 做边缘卷积，confuse 反色，shake 做加权模糊；无效果时直接复制场景像素。

in vec2 TexCoords;
out vec4 color;

uniform sampler2D scene;
uniform vec2  offsets[9];
uniform int     edge_kernel[9];
uniform float  blur_kernel[9];

uniform bool chaos;
uniform bool confuse;
uniform bool shake;

void main()
{
    // zero out memory since an out variable is initialized with undefined values by default 
    color = vec4(0.0f);

    vec3 sample[9];
    // 仅卷积效果需要九次邻域采样；反色与普通路径各读取中心纹理一次。
    // sample from texture offsets if using convolution matrix
    if(chaos || shake)
        for(int i = 0; i < 9; i++)
            sample[i] = vec3(texture(scene, TexCoords.st + offsets[i]));

    // process effects
    if(chaos)
    {           
        for(int i = 0; i < 9; i++)
            color += vec4(sample[i] * edge_kernel[i], 0.0f);
        color.a = 1.0f;
    }
    else if(confuse)
    {
        color = vec4(1.0 - texture(scene, TexCoords).rgb, 1.0);
    }
    else if(shake)
    {
        for(int i = 0; i < 9; i++)
            color += vec4(sample[i] * blur_kernel[i], 0.0f);
        color.a = 1.0f;
    }
    else
    {
        color =  texture(scene, TexCoords);
    }
}
