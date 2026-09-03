#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：后处理顶点着色器，直接输出全屏四边形并按效果修改采样坐标或屏幕位置。
// 输入输出：vertex 包含 NDC 位置与场景纹理坐标，time/chaos/confuse/shake 控制动画，TexCoords 传给片段阶段。
// 核心算法：chaos 周期偏移纹理坐标，confuse 翻转 UV，shake 对裁剪空间位置施加微小时间振荡。

layout (location = 0) in vec4 vertex; // <vec2 position, vec2 texCoords>

out vec2 TexCoords;

uniform bool chaos;
uniform bool confuse;
uniform bool shake;
uniform float time;

void main()
{
    gl_Position = vec4(vertex.xy, 0.0f, 1.0f); 
    vec2 texture = vertex.zw;
    if(chaos)
    {
        float strength = 0.3;
        vec2 pos = vec2(texture.x + sin(time) * strength, texture.y + cos(time) * strength);        
        TexCoords = pos;
    }
    else if(confuse)
    {
        TexCoords = vec2(1.0 - texture.x, 1.0 - texture.y);
    }
    else
    {
        TexCoords = texture;
    }
    if (shake)
    {
        float strength = 0.01;
        gl_Position.x += cos(time * 10) * strength;        
        gl_Position.y += cos(time * 15) * strength;        
    }
}