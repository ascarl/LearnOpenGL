/*******************************************************************
** This code is part of Breakout.
**
** Breakout is free software: you can redistribute it and/or modify
** it under the terms of the CC BY 4.0 license as published by
** Creative Commons, either version 4 of the License, or (at your
** option) any later version.
******************************************************************/
// LearnOpenGL 中文导读
// 学习目标：实现 GameObject 的默认状态、参数化构造和渲染委托。
// 核心流程：构造函数只保存逻辑/渲染属性；Draw 不拥有批次或 GPU 状态，而是调用共享 SpriteRenderer。
// 生命周期：Destroyed 是逻辑删除标记，关卡和道具容器决定对象何时真正移除。

#include "game_object.h"


GameObject::GameObject() 
    : Position(0.0f, 0.0f), Size(1.0f, 1.0f), Velocity(0.0f), Color(1.0f), Rotation(0.0f), Sprite(), IsSolid(false), Destroyed(false) { }

GameObject::GameObject(glm::vec2 pos, glm::vec2 size, Texture2D sprite, glm::vec3 color, glm::vec2 velocity) 
    : Position(pos), Size(size), Velocity(velocity), Color(color), Rotation(0.0f), Sprite(sprite), IsSolid(false), Destroyed(false) { }

void GameObject::Draw(SpriteRenderer &renderer)
{
    // 实体不直接操作 OpenGL，统一由渲染器把对象状态转换成 model 矩阵和一次精灵绘制。
    renderer.DrawSprite(this->Sprite, this->Position, this->Size, this->Rotation, this->Color);
}