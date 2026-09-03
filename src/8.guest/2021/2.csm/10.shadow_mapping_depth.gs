#version 410 core
// LearnOpenGL 中文导读
// 着色阶段：CSM 分层深度 Pass 的几何着色器；每个输入三角形产生 5 次 invocation，每次最多输出一个三角形。
// 输入输出：读取顶点阶段给出的三个世界空间顶点和 UBO 中对应级联矩阵；输出到由 gl_Layer 选择的深度纹理数组层。
// 数据流：gl_InvocationID 同时索引 lightSpaceMatrices 和目标 layer，使一次场景提交并行复制到五张级联阴影图。


layout(triangles, invocations = 5) in;
layout(triangle_strip, max_vertices = 3) out;

layout (std140) uniform LightSpaceMatrices
{
    mat4 lightSpaceMatrices[16];
};
/*
uniform mat4 lightSpaceMatrices[16];
*/

void main()
{          
	// 每个 invocation 独立遍历原三角形三个顶点，只写自己的级联层，不在 invocation 之间共享可变数据。
	for (int i = 0; i < 3; ++i)
	{
		gl_Position = lightSpaceMatrices[gl_InvocationID] * gl_in[i].gl_Position;
		gl_Layer = gl_InvocationID;
		EmitVertex();
	}
	EndPrimitive();
}  
