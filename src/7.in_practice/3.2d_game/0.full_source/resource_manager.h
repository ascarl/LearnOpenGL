/*******************************************************************
** This code is part of Breakout.
**
** Breakout is free software: you can redistribute it and/or modify
** it under the terms of the CC BY 4.0 license as published by
** Creative Commons, either version 4 of the License, or (at your
** option) any later version.
******************************************************************/
// LearnOpenGL 中文导读
// 学习目标：理解以字符串名称缓存 Shader 与 Texture2D 的静态资源管理器。
// 核心流程：Load 从文件创建 GPU 对象并写入映射，Get 返回轻量句柄副本，Clear 统一删除 OpenGL 对象。
// 生命周期：ResourceManager 本身不可实例化；Clear 必须在有效 OpenGL 上下文仍存在时调用，并且只覆盖映射中的 Shader 程序与 Texture2D。
// 所有权边界：精灵、粒子、后处理和文字渲染器自行创建的 VAO/VBO、FBO/RBO、后处理纹理及字形纹理都不属于这两个映射，Clear 不会释放它们。

#ifndef RESOURCE_MANAGER_H
#define RESOURCE_MANAGER_H

#include <map>
#include <string>

#include <glad/glad.h>

#include "texture.h"
#include "shader.h"


// A static singleton ResourceManager class that hosts several
// functions to load Textures and Shaders. Each loaded texture
// and/or shader is also stored for future reference by string
// handles. All functions and resources are static and no 
// public constructor is defined.
class ResourceManager
{
public:
    // 名称是游戏模块之间的资源契约，避免各对象重复读取文件和创建相同 GPU 资源。
    // resource storage
    static std::map<std::string, Shader>    Shaders;
    static std::map<std::string, Texture2D> Textures;
    // loads (and generates) a shader program from file loading vertex, fragment (and geometry) shader's source code. If gShaderFile is not nullptr, it also loads a geometry shader
    static Shader    LoadShader(const char *vShaderFile, const char *fShaderFile, const char *gShaderFile, std::string name);
    // retrieves a stored sader
    static Shader    GetShader(std::string name);
    // loads (and generates) a texture from file
    static Texture2D LoadTexture(const char *file, bool alpha, std::string name);
    // retrieves a stored texture
    static Texture2D GetTexture(std::string name);
    // properly de-allocates all loaded resources
    static void      Clear();
private:
    // private constructor, that is we do not want any actual resource manager objects. Its members and functions should be publicly available (static).
    ResourceManager() { }
    // loads and generates a shader from file
    static Shader    loadShaderFromFile(const char *vShaderFile, const char *fShaderFile, const char *gShaderFile = nullptr);
    // loads a single texture from file
    static Texture2D loadTextureFromFile(const char *file, bool alpha);
};

#endif