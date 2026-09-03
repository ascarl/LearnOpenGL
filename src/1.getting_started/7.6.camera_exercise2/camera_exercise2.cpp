// LearnOpenGL 中文导读
// 文件性质：这是 7.1/7.4 中 glm::lookAt 的手工实现练习答案，不是独立 C++ 程序。
// 与基础示例的精确差异：自行由 position、target、worldUp 求相机右/上/后轴，并返回 rotation*translation 观察矩阵。
// 观察重点：GLM 按列存储，最终矩阵从右向左先把世界平移到相机原点，再旋转到相机基底。

// Custom implementation of the LookAt function
glm::mat4 calculate_lookAt_matrix(glm::vec3 position, glm::vec3 target, glm::vec3 worldUp)
{
    // 1. Position = known
    // 2. Calculate cameraDirection
    // 相机后轴指向 position-target；OpenGL 相机默认观察局部 -Z，因此观察矩阵使用这条 +Z 后轴。
    glm::vec3 zaxis = glm::normalize(position - target);
    // 3. Get positive right axis vector
    // worldUp×z 得右轴，再由 z×x 得校正后的上轴，三者构成正交相机基底。
    glm::vec3 xaxis = glm::normalize(glm::cross(glm::normalize(worldUp), zaxis));
    // 4. Calculate camera up vector
    glm::vec3 yaxis = glm::cross(zaxis, xaxis);

    // Create translation and rotation matrix
    // In glm we access elements as mat[col][row] due to column-major layout
    // GLM 的 [列][行] 索引决定这里的填充方向；平移项使用 -position 把相机移到原点。
    glm::mat4 translation = glm::mat4(1.0f); // Identity matrix by default
    translation[3][0] = -position.x; // Fourth column, first row
    translation[3][1] = -position.y;
    translation[3][2] = -position.z;
    glm::mat4 rotation = glm::mat4(1.0f);
    rotation[0][0] = xaxis.x; // First column, first row
    rotation[1][0] = xaxis.y;
    rotation[2][0] = xaxis.z;
    rotation[0][1] = yaxis.x; // First column, second row
    rotation[1][1] = yaxis.y;
    rotation[2][1] = yaxis.z;
    rotation[0][2] = zaxis.x; // First column, third row
    rotation[1][2] = zaxis.y;
    rotation[2][2] = zaxis.z; 

    // Return lookAt matrix as combination of translation and rotation matrix
    // 组合顺序 rotation*translation：列向量先平移到相机原点，再投影到相机右/上/后轴。
    return rotation * translation; // Remember to read from right to left (first translation then rotation)
}


// Don't forget to replace glm::lookAt with your own version
// view = glm::lookAt(glm::vec3(camX, 0.0f, camZ), glm::vec3(0.0f, 0.0f, 0.0f), glm::vec3(0.0f, 1.0f, 0.0f));
view = calculate_lookAt_matrix(glm::vec3(camX, 0.0f, camZ), glm::vec3(0.0f, 0.0f, 0.0f), glm::vec3(0.0f, 1.0f, 0.0f));