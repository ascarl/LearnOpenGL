#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：多 LTC 面光源受光平面的顶点着色器，每个平面顶点执行一次。
// 输入输出：输出世界空间位置、世界空间法线和 UV，并用 projection/view/model 生成裁剪空间位置。
// 数据流：同一世界空间表面属性会被片段阶段用于遍历 areaLights 数组并累加所有光源贡献。


layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexcoord;

uniform mat4 model;
uniform mat3 normalMatrix;
uniform mat4 view;
uniform mat4 projection;

out vec3 worldPosition;
out vec3 worldNormal;
out vec2 texcoord;

void main()
{
	vec4 worldpos = model * vec4(aPosition, 1.0f);
	worldPosition = worldpos.xyz;
	worldNormal = normalMatrix * aNormal;
	texcoord = aTexcoord;

	gl_Position = projection * view * worldpos;
}
