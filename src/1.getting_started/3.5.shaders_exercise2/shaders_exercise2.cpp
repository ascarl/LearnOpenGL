// LearnOpenGL 中文导读
// 文件性质：这是 3.3 的 CPU 设置代码与顶点着色器组合答案，不是独立 C++ 程序。
// 与基础示例的精确差异：CPU 将 xOffset=0.5 上传为 uniform，顶点着色器只给 aPos.x 加该偏移，整体向右平移。
// 观察重点：uniform 在一次绘制的所有顶点间共享，因此不会改变三角形形状。

// In your CPP file:
// ======================
float offset = 0.5f;
// Program 状态：Shader::setFloat 只用 ourShader.ID 查询 location，内部 glUniform1f 仍写入当前绑定的 Program，并不会自动切换。
// 可靠用法必须先调用 ourShader.use()；若承接 3.3 时恰好仍绑定同一 Program，这段才会工作，属于脆弱的隐式状态依赖。
ourShader.setFloat("xOffset", offset);

// In your vertex shader:
// ======================
#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aColor;

out vec3 ourColor;

// 数据边界：CPU 的 setFloat 按名称写入此 uniform，顶点阶段的每次调用读取同一个值。
uniform float xOffset;

void main()
{
    // 关键步骤：在写入裁剪空间前统一平移 x，未修改原始 VBO。
    gl_Position = vec4(aPos.x + xOffset, aPos.y, aPos.z, 1.0); // add the xOffset to the x position of the vertex position
    ourColor = aColor;
}