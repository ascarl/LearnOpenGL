#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：环境背景片段着色器，按世界方向读取 HDR Cubemap 并转换到显示空间。
// 输入输出：environmentMap 是转换后的浮点环境图，WorldPos 只表示方向而非有限位置。
// 核心算法：先对线性 HDR 做 Reinhard 映射，再以 1/2.2 幂近似 sRGB 编码；不把显示编码反馈给 IBL 积分。
out vec4 FragColor;
in vec3 WorldPos;

uniform samplerCube environmentMap;

void main()
{		
    vec3 envColor = textureLod(environmentMap, WorldPos, 0.0).rgb;
    
    // HDR tonemap and gamma correct
    envColor = envColor / (envColor + vec3(1.0));
    envColor = pow(envColor, vec3(1.0/2.2)); 
    
    FragColor = vec4(envColor, 1.0);
}
