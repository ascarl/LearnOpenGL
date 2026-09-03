/*******************************************************************
** This code is part of Breakout.
**
** Breakout is free software: you can redistribute it and/or modify
** it under the terms of the CC BY 4.0 license as published by
** Creative Commons, either version 4 of the License, or (at your
** option) any later version.
******************************************************************/
// LearnOpenGL 中文导读
// 学习目标：用继承复用 GameObject 的移动和绘制状态，并补充道具类型、持续时间与激活状态。
// 核心流程：speed 与 pad-size-increase 的 Duration 为 0，拾取时立即累加速度或挡板宽度；sticky、pass-through、confuse、chaos 才是定时效果。
// 生命周期：两种即时效果很快把 Activated 置回 false，却不会反向扣除增量，效果一直保留到 ResetPlayer；四种定时效果会倒计时、跟踪同类重叠并最终撤销。
// 容器状态：Destroyed 表示屏幕实体不可再绘制，Activated 表示仍需跟踪定时状态；两者满足清理条件后才移出容器。

#ifndef POWER_UP_H
#define POWER_UP_H
#include <string>

#include <glad/glad.h>
#include <glm/glm.hpp>

#include "game_object.h"


// The size of a PowerUp block
const glm::vec2 POWERUP_SIZE(60.0f, 20.0f);
// Velocity a PowerUp block has when spawned
const glm::vec2 VELOCITY(0.0f, 150.0f);


// PowerUp inherits its state and rendering functions from
// GameObject but also holds extra information to state its
// active duration and whether it is activated or not. 
// The type of PowerUp is stored as a string.
class PowerUp : public GameObject 
{
public:
    // powerup state
    std::string Type;
    float       Duration;	
    bool        Activated;
    // constructor
    PowerUp(std::string type, glm::vec3 color, float duration, glm::vec2 position, Texture2D texture) 
        : GameObject(position, POWERUP_SIZE, texture, color, VELOCITY), Type(type), Duration(duration), Activated() { }
};

#endif