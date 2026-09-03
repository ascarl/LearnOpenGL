#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：调试片段着色器，把方向光深度纹理转换为灰度输出。
// 输入输出：depthMap 来自光源视角深度 Pass，TexCoords 是屏幕四边形坐标；near/far 描述光源裁剪范围。
// 核心算法：正交投影下深度本就与距离线性对应；灰度值用于检查覆盖范围、精度与深度附件内容。
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D depthMap;
uniform float near_plane;
uniform float far_plane;

// required when using a perspective projection matrix
float LinearizeDepth(float depth)
{
    float z = depth * 2.0 - 1.0; // Back to NDC 
    return (2.0 * near_plane * far_plane) / (far_plane + near_plane - z * (far_plane - near_plane));	
}

void main()
{             
    float depthValue = texture(depthMap, TexCoords).r;
    // FragColor = vec4(vec3(LinearizeDepth(depthValue) / far_plane), 1.0); // perspective
    FragColor = vec4(vec3(depthValue), 1.0); // orthographic
}