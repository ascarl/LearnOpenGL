#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：SSAO 模糊片段着色器，对原始遮蔽图执行 4x4 盒式平均。
// 输入输出：ssaoInput 是噪声较大的单通道 SSAO 附件，FragColor 写入 ssaoColorBufferBlur。
// Pass 依赖：最终光照只读取该平滑结果，并用它缩放环境光而非直接光。
out float FragColor;

in vec2 TexCoords;

uniform sampler2D ssaoInput;

void main() 
{
    vec2 texelSize = 1.0 / vec2(textureSize(ssaoInput, 0));
    float result = 0.0;
    for (int x = -2; x < 2; ++x) 
    {
        for (int y = -2; y < 2; ++y) 
        {
            vec2 offset = vec2(float(x), float(y)) * texelSize;
            result += texture(ssaoInput, TexCoords + offset).r;
        }
    }
    FragColor = result / (4.0 * 4.0);
}  