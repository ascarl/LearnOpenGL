#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：SSAO 几何 Pass 片段着色器，写入视空间 G-buffer。
// 输入输出：location 0=gPosition，1=gNormal，2=gAlbedo；本示例基础色固定为浅灰。
// Pass 依赖：SSAO Pass 读取前两附件，最终光照 Pass 再同时读取三附件和模糊遮蔽图。
layout (location = 0) out vec3 gPosition;
layout (location = 1) out vec3 gNormal;
layout (location = 2) out vec3 gAlbedo;

in vec2 TexCoords;
in vec3 FragPos;
in vec3 Normal;

void main()
{    
    // store the fragment position vector in the first gbuffer texture
    gPosition = FragPos;
    // also store the per-fragment normals into the gbuffer
    gNormal = normalize(Normal);
    // and the diffuse per-fragment color
    gAlbedo.rgb = vec3(0.95);
}