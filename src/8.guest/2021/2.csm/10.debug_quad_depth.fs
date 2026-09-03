#version 410 core
// LearnOpenGL 中文导读
// 着色阶段：级联深度层可视化片段着色器，每个屏幕像素执行一次。
// 输入输出：按 UV 和 uniform layer 采样 sampler2DArray；把正交光投影存储的深度直接输出为灰度。
// 核心算法：LinearizeDepth 仅供透视深度调试；当前级联使用正交投影，所以直接显示采样值。

out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2DArray depthMap;
uniform float near_plane;
uniform float far_plane;
uniform int layer;

// required when using a perspective projection matrix
float LinearizeDepth(float depth)
{
    float z = depth * 2.0 - 1.0; // Back to NDC 
    return (2.0 * near_plane * far_plane) / (far_plane + near_plane - z * (far_plane - near_plane));	
}

void main()
{             
    float depthValue = texture(depthMap, vec3(TexCoords, layer)).r;
    // FragColor = vec4(vec3(LinearizeDepth(depthValue) / far_plane), 1.0); // perspective
    FragColor = vec4(vec3(depthValue), 1.0); // orthographic
}
