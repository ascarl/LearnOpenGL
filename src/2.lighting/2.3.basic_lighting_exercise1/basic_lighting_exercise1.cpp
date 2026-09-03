// LearnOpenGL 中文导读
// 学习目标：让点光源随时间移动，观察同一套漫反射/镜面公式如何响应动态 lightPos。
// 核心流程：该文件是 render loop 的参考片段，以正弦函数更新 x/y，再在后续 uniform 设置中传给 Shader。
// 本练习新增：光源位置变为逐帧状态；其更新必须发生在本帧上传 lightPos 和绘制物体之前。
// 观察重点：这是带 [...] 省略段的教学答案，不是可单独构建的完整 C++ 程序。
int main()
{
    [...]
    // render loop
    while(!glfwWindowShouldClose(window))
    {
        // per-frame time logic
        float currentFrame = glfwGetTime();
        deltaTime = currentFrame - lastFrame;
        lastFrame = currentFrame;

        // input
        processInput(window);

        // clear the colorbuffer
        glClearColor(0.1f, 0.1f, 0.1f, 1.0f);
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

        // change the light's position values over time (can be done anywhere in the render loop actually, but try to do it at least before using the light source positions)
        // x 与 y 使用不同频率的正弦函数，使光源沿周期轨迹移动；z 保持原值。
        lightPos.x = 1.0f + sin(glfwGetTime()) * 2.0f;
        lightPos.y = sin(glfwGetTime() / 2.0f) * 1.0f;
        
        // set uniforms, draw objects
        // 省略部分应在这里把更新后的 lightPos 同时用于光照 uniform 和灯标记 model 矩阵。
        [...]
        
        // glfw: swap buffers and poll IO events
        glfwSwapBuffers(window);
        glfwPollEvents();
    }
}