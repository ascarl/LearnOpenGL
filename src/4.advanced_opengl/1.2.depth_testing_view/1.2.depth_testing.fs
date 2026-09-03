#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器，把硬件提供的窗口深度转换为可观察的灰度。
// 输入输出：gl_FragCoord.z 位于 [0,1]；输出 FragColor 写入默认帧缓冲颜色附件。
// 核心算法：先把深度还原到 [-1,1] 的 NDC，再结合 near/far 反解观察空间线性距离并归一化。
out vec4 FragColor;

float near = 0.1; 
float far = 100.0; 
float LinearizeDepth(float depth) 
{
    // 深度缓冲保存的是透视投影后的非线性值，不能直接当作与相机的线性距离。
    float z = depth * 2.0 - 1.0; // back to NDC 
    return (2.0 * near * far) / (far + near - z * (far - near));	
}

void main()
{             
    float depth = LinearizeDepth(gl_FragCoord.z) / far; // divide by far to get depth in range [0,1] for visualization purposes
    FragColor = vec4(vec3(depth), 1.0);
}