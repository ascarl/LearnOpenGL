# LearnOpenGL Core Code Comments Implementation Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** 为 LearnOpenGL 项目自有的 C/C++、公共头文件和 Shader 增加准确、分层的简体中文教学注释，同时证明没有改动可执行逻辑。

**Architecture:** 按课程章节拆分为互不重叠的文件集合，每个文件增加 `LearnOpenGL 中文导读` 文件头，并在复杂数据流处补充 `关键步骤` 或 `算法说明`。每批修改都通过覆盖率、纯注释差异和增量构建检查，最后执行全量构建与跨 Shader 阶段抽查。

**Tech Stack:** C++、C、OpenGL 3.3–4.5、GLSL、CMake、Git、macOS AppleClang。

---

## 通用注释模板与质量门禁

C/C++ 文件头采用以下结构，并根据真实代码填写内容：

```cpp
// LearnOpenGL 中文导读
// 学习目标：说明本示例新增或验证的 OpenGL 概念。
// 核心流程：用一至两行描述 CPU 数据准备、GPU 资源和绘制顺序。
// 观察重点：说明交互、画面差异、关键状态或坐标空间。
```

GLSL 文件保留 `#version` 位置，在其后采用：

```glsl
// LearnOpenGL 中文导读
// 着色阶段：顶点/片段/几何/细分/计算着色器。
// 输入输出：说明 attribute、varying、uniform、纹理或渲染目标。
// 核心算法：说明该阶段完成的坐标变换、光照、采样或后处理。
```

每项任务都执行以下质量门禁：

1. 使用 `rg --files` 获得该任务目录内的 `.cpp`、`.c`、`.h`、`.vs`、`.fs`、`.gs`、`.tcs`、`.tes`、`.cs` 文件。
2. 确认每个目标文件包含 `LearnOpenGL 中文导读`。
3. 运行 `git diff --check`。
4. 检查 `git diff --numstat` 的删除列为 `0`；本轮只插入注释和必要空行。
5. 检查统一差异中的新增非空行都以 `//` 开头，防止误改代码。
6. 运行 `cmake --build build-macos --parallel 8`，确认增量全量目标构建成功。

## Task 1: 入门章节

**Files:**
- Modify: `src/1.getting_started/` 下所有项目自有 C/C++ 与 Shader 文件。

**Step 1: 建立本章文件清单和教学点映射**

按目录名识别窗口创建、三角形、索引绘制、Shader、纹理、变换、坐标系和相机各示例相对前一示例新增的概念。

**Step 2: 增加文件级中文导读**

为每个入口文件解释初始化、资源上传和渲染循环；练习文件明确练习目标。为每个 Shader 解释 attribute、varying、uniform、纹理采样和 MVP 变换。

**Step 3: 增加关键路径说明**

重点解释 VAO/VBO/EBO 绑定关系、Shader 编译链接、纹理单元、矩阵乘法顺序、深度测试、delta time 和相机输入。

**Step 4: 运行本章质量门禁和全量增量构建**

Expected: 所有目标文件有中文导读；差异只新增注释；构建到 `100%`。

**Step 5: Commit**

```bash
git add src/1.getting_started
git commit -m "docs: annotate getting started examples"
```

## Task 2: 光照与模型加载

**Files:**
- Modify: `src/2.lighting/` 下所有 C/C++ 与 Shader 文件。
- Modify: `src/3.model_loading/` 下所有 C/C++ 与 Shader 文件。

**Step 1: 标注光照数据流**

解释法线、环境光/漫反射/镜面反射、材质、光照贴图、方向光/点光源/聚光灯和多光源累加。

**Step 2: 标注模型加载链路**

解释 Assimp 导入、Mesh 数据上传、材质纹理绑定和 Model/Mesh/Shader 之间的调用关系。

**Step 3: 运行两个章节的质量门禁和全量增量构建**

Expected: 纯注释差异且构建到 `100%`。

**Step 4: Commit**

```bash
git add src/2.lighting src/3.model_loading
git commit -m "docs: annotate lighting and model examples"
```

## Task 3: 高级 OpenGL

**Files:**
- Modify: `src/4.advanced_opengl/` 下所有 C/C++ 与 Shader 文件。

**Step 1: 标注状态与资源关系**

解释深度/模板测试、混合、面剔除、Framebuffer、Cubemap、Uniform Buffer、几何着色器、实例化和 MSAA。

**Step 2: 标注多 Pass 数据流**

明确每个 Pass 写入和读取的附件、纹理，以及屏幕空间后处理的执行顺序。

**Step 3: 运行本章质量门禁和全量增量构建**

Expected: 纯注释差异且构建到 `100%`。

**Step 4: Commit**

```bash
git add src/4.advanced_opengl
git commit -m "docs: annotate advanced OpenGL examples"
```

## Task 4: 高级光照与 PBR

**Files:**
- Modify: `src/5.advanced_lighting/` 下所有 C/C++ 与 Shader 文件。
- Modify: `src/6.pbr/` 下所有 C/C++ 与 Shader 文件。

**Step 1: 标注高级光照算法**

解释 Gamma、Shadow Mapping、法线/视差贴图、HDR、Bloom、延迟渲染和 SSAO 的空间、附件与 Pass 依赖。

**Step 2: 标注 PBR 数据流与公式**

解释 metallic/roughness/AO、Cook–Torrance BRDF、IBL 预计算、辐照度图、预过滤环境贴图和 BRDF LUT。

**Step 3: 运行两个章节的质量门禁和全量增量构建**

Expected: 纯注释差异且构建到 `100%`。

**Step 4: Commit**

```bash
git add src/5.advanced_lighting src/6.pbr
git commit -m "docs: annotate advanced lighting and PBR examples"
```

## Task 5: 实战章节

**Files:**
- Modify: `src/7.in_practice/` 下所有 C/C++、头文件与 Shader 文件。

**Step 1: 标注调试与文字渲染**

解释 OpenGL 错误检查、调试输出、FreeType 字形纹理和动态顶点更新。

**Step 2: 标注 2D 游戏架构**

解释 Game、ResourceManager、Renderer、GameObject、碰撞、粒子、后处理、音频和关卡数据之间的职责与生命周期。

**Step 3: 运行本章质量门禁和全量增量构建**

Expected: 纯注释差异且构建到 `100%`。

**Step 4: Commit**

```bash
git add src/7.in_practice
git commit -m "docs: annotate in-practice examples"
```

## Task 6: Guest 专题

**Files:**
- Modify: `src/8.guest/` 下所有 C/C++、头文件与 Shader 文件。

**Step 1: 标注专题算法和平台要求**

解释场景图、视锥剔除、骨骼动画、级联阴影、CPU/GPU 地形、Compute Shader、物理 Bloom 和面光源。仅对确实请求 4.2/4.3/4.5 的示例标明 macOS 系统 OpenGL 4.1 限制。

**Step 2: 标注特殊 Shader 阶段**

为几何、细分控制、细分求值和计算 Shader 说明执行粒度、输入输出及同步/图像访问语义。

**Step 3: 运行本章质量门禁和全量增量构建**

Expected: 所有文件可编译；高版本上下文示例只报告运行平台限制，不误报构建失败。

**Step 4: Commit**

```bash
git add src/8.guest
git commit -m "docs: annotate guest examples"
```

## Task 7: LearnOpenGL 公共辅助代码

**Files:**
- Modify: `includes/learnopengl/animation.h`
- Modify: `includes/learnopengl/animator.h`
- Modify: `includes/learnopengl/animdata.h`
- Modify: `includes/learnopengl/assimp_glm_helpers.h`
- Modify: `includes/learnopengl/bone.h`
- Modify: `includes/learnopengl/camera.h`
- Modify: `includes/learnopengl/entity.h`
- Modify: `includes/learnopengl/filesystem.h`
- Modify: `includes/learnopengl/mesh.h`
- Modify: `includes/learnopengl/model.h`
- Modify: `includes/learnopengl/model_animation.h`
- Modify: `includes/learnopengl/shader.h`
- Modify: `includes/learnopengl/shader_c.h`
- Modify: `includes/learnopengl/shader_m.h`
- Modify: `includes/learnopengl/shader_s.h`
- Modify: `includes/learnopengl/shader_t.h`

**Step 1: 标注职责和生命周期**

解释 Shader 程序、Camera、Mesh/Model、骨骼动画、场景实体与资源路径辅助类的所有权和典型调用链。

**Step 2: 标注易错边界**

重点说明 OpenGL 对象创建/绑定、Assimp 到 GLM 的转换、骨骼矩阵更新、纹理缓存与路径解析。

**Step 3: 运行公共头文件质量门禁和全量增量构建**

Expected: 所有引用这些头文件的目标重新编译并构建到 `100%`。

**Step 4: Commit**

```bash
git add includes/learnopengl
git commit -m "docs: annotate LearnOpenGL helper classes"
```

## Task 8: 全局覆盖率与行为不变验证

**Files:**
- Verify: `src/` 范围内项目自有源码和 Shader。
- Verify: `includes/learnopengl/` 下 16 个公共头文件。
- Preserve: `src/glad.c`
- Preserve: `src/stb_image.cpp`

**Step 1: 验证目标文件覆盖率**

Expected: 排除两个第三方文件后，所有目标文件都包含 `LearnOpenGL 中文导读`，没有漏项。

**Step 2: 验证纯注释差异**

从实现起点 `8957e64` 检查：源文件没有删除行，新增非空行全部为 `//` 注释；两个排除文件无差异。

**Step 3: 全量重新配置和构建**

```bash
cmake -S . -B build-macos -DCMAKE_BUILD_TYPE=Debug
cmake --build build-macos --parallel 8
```

Expected: 配置成功，全部目标构建到 `100%`。记录但不混淆既有 Assimp packed-member 警告。

**Step 4: 抽查注释准确性**

至少检查 `hello_triangle`、`camera_class`、`multiple_lights`、`model_loading`、`framebuffers`、`bloom`、`ibl_specular_textured`、`2d_game`、`csm`、`computeshader_helloworld` 及公共 `shader.h`/`model.h`/`animator.h`。

**Step 5: 汇总结果**

报告目标文件数、修改行数、各批提交、构建结果、未运行的高版本 OpenGL 示例及仍保持未跟踪的 `.DS_Store`。
