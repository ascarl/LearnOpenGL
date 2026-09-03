/*******************************************************************
** This code is part of Breakout.
**
** Breakout is free software: you can redistribute it and/or modify
** it under the terms of the CC BY 4.0 license as published by
** Creative Commons, either version 4 of the License, or (at your
** option) any later version.
******************************************************************/
// LearnOpenGL 中文导读
// 阶段快照：第 2 阶段，实现 Game 生命周期空壳，验证程序入口可以按固定顺序调用各阶段。
// 学习目标：把后续玩法逐步填入 Init、Update、ProcessInput 与 Render，而不改动 GLFW 主循环。
// 观察重点：空函数是本阶段刻意保留的扩展点，不代表完整游戏行为。

#include "game.h"

Game::Game(unsigned int width, unsigned int height) 
    : State(GAME_ACTIVE), Keys(), Width(width), Height(height)
{ 

}

Game::~Game()
{
    
}

void Game::Init()
{
   
}

void Game::Update(float dt)
{
    
}

void Game::ProcessInput(float dt)
{
   
}

void Game::Render()
{
    
}