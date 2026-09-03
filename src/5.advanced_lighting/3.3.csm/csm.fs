#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：片段着色器，将当前相机深度线性化为灰度。
// 输入输出：只使用 gl_FragCoord.z 与写死的 near/far，不读取阴影贴图或级联参数。
// 核心算法：由 NDC 深度反解视锥距离并除以 far；这是深度可视化，不是 Cascaded Shadow Mapping。
out vec4 color;

float LinearizeDepth(float depth) // Note that this ranges from [0,1] instead of up to 'far plane distance' since we divide by 'far'
{
    float near = 0.1; 
    float far = 100.0; 
    float z = depth * 2.0 - 1.0; // Back to NDC 
    return (2.0 * near) / (far + near - z * (far - near));	
}

void main()
{             
    float depth = LinearizeDepth(gl_FragCoord.z);
    color = vec4(vec3(depth), 1.0f);
}