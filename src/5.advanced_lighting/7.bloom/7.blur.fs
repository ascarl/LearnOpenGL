#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：可分离高斯模糊片段着色器，沿单一轴卷积高亮纹理。
// 输入输出：image 是上一 Pass 附件，horizontal 选择横向或纵向，FragColor 写入另一张 ping-pong 纹理。
// 核心算法：对中心及两侧四组 texel 使用对称高斯权重；交替两轴把二维卷积成本降为两次一维卷积。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D image;

uniform bool horizontal;
// 权重已归一化为中心一次加两侧各四次采样，总能量约为 1，避免反复模糊改变整体亮度。
uniform float weight[5] = float[] (0.2270270270, 0.1945945946, 0.1216216216, 0.0540540541, 0.0162162162);

void main()
{             
     vec2 tex_offset = 1.0 / textureSize(image, 0); // gets size of single texel
     vec3 result = texture(image, TexCoords).rgb * weight[0];
     if(horizontal)
     {
         for(int i = 1; i < 5; ++i)
         {
            result += texture(image, TexCoords + vec2(tex_offset.x * i, 0.0)).rgb * weight[i];
            result += texture(image, TexCoords - vec2(tex_offset.x * i, 0.0)).rgb * weight[i];
         }
     }
     else
     {
         for(int i = 1; i < 5; ++i)
         {
             result += texture(image, TexCoords + vec2(0.0, tex_offset.y * i)).rgb * weight[i];
             result += texture(image, TexCoords - vec2(0.0, tex_offset.y * i)).rgb * weight[i];
         }
     }
     FragColor = vec4(result, 1.0);
}