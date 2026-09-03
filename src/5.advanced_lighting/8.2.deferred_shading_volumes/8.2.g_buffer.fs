#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：延迟着色几何 Pass 片段着色器，填充 G-buffer 的三个 MRT 附件。
// 输入输出：location 0=gPosition 世界位置，1=gNormal 世界法线，2=gAlbedoSpec 的 RGB/Alpha=漫反射/镜面强度。
// Pass 依赖：屏幕光照 Pass 必须按完全相同的空间和通道约定读取这些附件。
layout (location = 0) out vec3 gPosition;
layout (location = 1) out vec3 gNormal;
layout (location = 2) out vec4 gAlbedoSpec;

in vec2 TexCoords;
in vec3 FragPos;
in vec3 Normal;

uniform sampler2D texture_diffuse1;
uniform sampler2D texture_specular1;

void main()
{    
    // store the fragment position vector in the first gbuffer texture
    gPosition = FragPos;
    // also store the per-fragment normals into the gbuffer
    gNormal = normalize(Normal);
    // and the diffuse per-fragment color
    gAlbedoSpec.rgb = texture(texture_diffuse1, TexCoords).rgb;
    // store specular intensity in gAlbedoSpec's alpha component
    gAlbedoSpec.a = texture(texture_specular1, TexCoords).r;
}