#version 330 core
// LearnOpenGL 中文导读
// 着色阶段：环境映射片段着色器，以世界空间反射方向采样 Cubemap。
// 输入输出：Position、Normal 与 cameraPos 均为世界空间；skybox 返回环境颜色，FragColor 写入默认颜色附件。
// 核心算法：I 从相机指向片段，reflect(I,N) 得到镜面反射方向；折射版本可使用 refract(I,N,eta)。
out vec4 FragColor;

in vec3 Normal;
in vec3 Position;

uniform vec3 cameraPos;
uniform samplerCube skybox;

void main()
{    
    // 统一空间后再计算方向，否则模型变换会导致反射随物体姿态出现错误偏移。
    vec3 I = normalize(Position - cameraPos);
    vec3 R = reflect(I, normalize(Normal));
    FragColor = vec4(texture(skybox, R).rgb, 1.0);
}