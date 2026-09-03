#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：BRDF LUT 预计算的全屏顶点着色器，输出裁剪空间四边形与二维参数坐标。
// 输入输出：TexCoords.x=NdotV，TexCoords.y=roughness，覆盖 LUT 的完整 [0,1]² 定义域。
// Pass 依赖：片段阶段积分几何/Fresnel 响应，结果写入 RG16F brdfLUTTexture。
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

void main()
{
    TexCoords = aTexCoords;
	gl_Position = vec4(aPos, 1.0);
}