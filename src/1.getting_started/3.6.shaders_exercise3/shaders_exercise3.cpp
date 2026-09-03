// LearnOpenGL 中文导读
// 文件性质：这是 3.3 的顶点/片段着色器组合答案，不是独立 C++ 程序。
// 与基础示例的精确差异：跨阶段传递 aPos 而非 aColor，并将插值后的位置 xyz 直接作为 RGB。
// 观察重点：负坐标写入颜色时被钳制到 0，所以含负 x、y 的左下区域呈黑色。

// Vertex shader:
// ==============
#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aColor;

// out vec3 ourColor;
out vec3 ourPosition;

void main()
{
    gl_Position = vec4(aPos, 1.0); 
    // ourColor = aColor;
    // 数据边界：位置作为 varying 输出，光栅化器会为每个片段插值该值。
    ourPosition = aPos;
}

// Fragment shader:
// ================
#version 330 core
out vec4 FragColor;
// in vec3 ourColor;
in vec3 ourPosition;

void main()
{
    // 关键步骤：插值位置直接映射到 RGB；默认帧缓冲会把负颜色分量钳制为 0。
    FragColor = vec4(ourPosition, 1.0);    // note how the position value is linearly interpolated to get all the different colors
}

/* 
Answer to the question: Do you know why the bottom-left side is black?
-- --------------------------------------------------------------------
Think about this for a second: the output of our fragment's color is equal to the (interpolated) coordinate of 
the triangle. What is the coordinate of the bottom-left point of our triangle? This is (-0.5f, -0.5f, 0.0f). Since the
xy values are negative they are clamped to a value of 0.0f. This happens all the way to the center sides of the 
triangle since from that point on the values will be interpolated positively again. Values of 0.0f are of course black
and that explains the black side of the triangle.
*/