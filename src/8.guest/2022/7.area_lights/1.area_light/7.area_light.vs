#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：LTC 受光平面的顶点着色器，每个平面顶点执行一次。
// 输入输出：读取局部位置/法线/UV，输出世界空间位置、normalMatrix 变换后的世界法线与 UV，并生成裁剪空间位置。
// 数据流：片段阶段的 LTC 积分统一在世界空间完成，因此位置、法线、相机和光源四角必须处于同一空间。


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
