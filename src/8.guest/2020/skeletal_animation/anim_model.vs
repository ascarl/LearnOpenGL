#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：骨骼蒙皮顶点着色器，每个网格顶点执行一次。
// 输入输出：读取位置、法线、UV、四个 boneIds/weights 和 CPU 上传的 finalBonesMatrices；输出裁剪空间位置与 UV。
// 核心算法：在模型局部空间累加最多四个骨骼变换后的加权位置，再应用 model、view、projection。

layout(location = 0) in vec3 pos;
layout(location = 1) in vec3 norm;
layout(location = 2) in vec2 tex;
layout(location = 3) in vec3 tangent;
layout(location = 4) in vec3 bitangent;
layout(location = 5) in ivec4 boneIds; 
layout(location = 6) in vec4 weights;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

const int MAX_BONES = 100;
const int MAX_BONE_INFLUENCE = 4;
uniform mat4 finalBonesMatrices[MAX_BONES];

out vec2 TexCoords;

void main()
{
    vec4 totalPosition = vec4(0.0f);
    // 每个槽位独立查找对应骨骼矩阵；-1 表示该槽没有骨骼影响，越界则回退到原始位置。
    for(int i = 0 ; i < MAX_BONE_INFLUENCE ; i++)
    {
        if(boneIds[i] == -1) 
            continue;
        if(boneIds[i] >=MAX_BONES) 
        {
            totalPosition = vec4(pos,1.0f);
            break;
        }
        vec4 localPosition = finalBonesMatrices[boneIds[i]] * vec4(pos,1.0f);
        // 线性混合蒙皮要求有效权重之和接近 1，最终位置才不会被额外缩放。
        totalPosition += localPosition * weights[i];
        vec3 localNormal = mat3(finalBonesMatrices[boneIds[i]]) * norm;
   }
	
    mat4 viewModel = view * model;
    gl_Position =  projection * viewModel * totalPosition;
	TexCoords = tex;
}
