#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：精灵顶点着色器，把共享单位四边形变换为屏幕中的游戏对象。
// 输入输出：vertex.xy 是 0..1 局部位置、vertex.zw 是纹理坐标，model/projection 为 uniform，TexCoords 传给片段阶段。
// 坐标空间：model 完成尺寸、绕中心旋转和平移，projection 把左上原点的像素坐标映射到裁剪空间；固定相机无需 view。

layout (location = 0) in vec4 vertex; // <vec2 position, vec2 texCoords>

out vec2 TexCoords;

uniform mat4 model;
// note that we're omitting the view matrix; the view never changes so we basically have an identity view matrix and can therefore omit it.
uniform mat4 projection;

void main()
{
    TexCoords = vertex.zw;
    gl_Position = projection * model * vec4(vertex.xy, 0.0, 1.0);
}