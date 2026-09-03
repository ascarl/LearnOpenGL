#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：点光源深度 Pass 的片段着色器，将世界空间径向距离编码进深度 Cubemap。
// 输入输出：FragPos 与 lightPos 均为世界空间，far_plane 把距离归一化到深度缓冲的 [0,1]。
// 核心算法：gl_FragDepth=length(FragPos-lightPos)/far_plane；相机 Pass 必须乘回同一 far_plane 后比较。
in vec4 FragPos;

uniform vec3 lightPos;
uniform float far_plane;

void main()
{
    // FragPos 与 lightPos 都在世界空间，所以长度是真实场景单位下的径向距离。
    float lightDistance = length(FragPos.xyz - lightPos);
    
    // map to [0;1] range by dividing by far_plane
    // 除以 far_plane 后写入 [0,1] 深度；相机 Pass 会乘回同一尺度后比较。
    lightDistance = lightDistance / far_plane;
    
    // write this as modified depth
    gl_FragDepth = lightDistance;
}