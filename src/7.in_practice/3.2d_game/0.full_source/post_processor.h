/*******************************************************************
** This code is part of Breakout.
**
** Breakout is free software: you can redistribute it and/or modify
** it under the terms of the CC BY 4.0 license as published by
** Creative Commons, either version 4 of the License, or (at your
** option) any later version.
******************************************************************/
// LearnOpenGL 中文导读
// 学习目标：理解多重采样离屏渲染、颜色 resolve 与全屏后处理三个阶段。
// 核心流程：BeginRender 绑定 MSFBO，EndRender blit 到可采样纹理，Render 在全屏四边形上执行特效 Shader。
// 调用顺序：对象持有两个 FBO、一个多采样 RBO、resolve 纹理和屏幕 VAO；BeginRender、EndRender、Render 的顺序是正确工作的前提。
// 当前限制：类没有析构函数，MSFBO/FBO/RBO、resolve Texture、VAO 与 initRenderData 的局部 VBO 均没有对应的 GPU 删除路径。

#ifndef POST_PROCESSOR_H
#define POST_PROCESSOR_H

#include <glad/glad.h>
#include <glm/glm.hpp>

#include "texture.h"
#include "sprite_renderer.h"
#include "shader.h"


// PostProcessor hosts all PostProcessing effects for the Breakout
// Game. It renders the game on a textured quad after which one can
// enable specific effects by enabling either the Confuse, Chaos or 
// Shake boolean. 
// It is required to call BeginRender() before rendering the game
// and EndRender() after rendering the game for the class to work.
class PostProcessor
{
public:
    // state
    Shader PostProcessingShader;
    Texture2D Texture;
    unsigned int Width, Height;
    // options
    bool Confuse, Chaos, Shake;
    // constructor
    PostProcessor(Shader shader, unsigned int width, unsigned int height);
    // prepares the postprocessor's framebuffer operations before rendering the game
    void BeginRender();
    // should be called after rendering the game, so it stores all the rendered data into a texture object
    void EndRender();
    // renders the PostProcessor texture quad (as a screen-encompassing large sprite)
    void Render(float time);
private:
    // render state
    // MSFBO 接收多重采样场景，FBO 的单采样 Texture 才能被后处理 Shader 读取。
    unsigned int MSFBO, FBO; // MSFBO = Multisampled FBO. FBO is regular, used for blitting MS color-buffer to texture
    unsigned int RBO; // RBO is used for multisampled color buffer
    unsigned int VAO;
    // initialize quad for rendering postprocessing texture
    void initRenderData();
};

#endif