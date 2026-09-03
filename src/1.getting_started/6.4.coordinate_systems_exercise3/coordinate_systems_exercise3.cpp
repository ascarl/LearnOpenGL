// LearnOpenGL 中文导读
// 文件性质：这是 6.3 多立方体绘制循环的练习答案片段，不是独立 C++ 程序。
// 与基础示例的精确差异：默认仍用 20°*索引的静态角度，但索引能被 3 整除的立方体改用 time*25° 持续旋转。
// 观察重点：只替换部分对象的 Model 旋转角，View/Projection、网格和绘制调用均不变。

...


glBindVertexArray(VAO);
for(unsigned int i = 0; i < 10; i++)
{
    // calculate the model matrix for each object and pass it to shader before drawing
    // 每个对象仍复用同一 VAO，循环只为本次 draw 更新独立的 Model 矩阵。
    glm::mat4 model = glm::mat4(1.0f);
    model = glm::translate(model, cubePositions[i]);
    float angle = 20.0f * i; 
    // 关键差异：索引 0、3、6、9 使用时间角度，其余对象保持由索引决定的静态角度。
    if(i % 3 == 0)  // every 3rd iteration (including the first) we set the angle using GLFW's time function.
        angle = glfwGetTime() * 25.0f;
    model = glm::rotate(model, glm::radians(angle), glm::vec3(1.0f, 0.3f, 0.5f));
    ourShader.setMat4("model", model);
    
    glDrawArrays(GL_TRIANGLES, 0, 36);           
}

...