# LearnOpenGL 学习仓库使用指引

本仓库收录了 [LearnOpenGL](https://learnopengl.com/) 各章节的 C++ 示例。它更像一组与教程逐节对应的“代码快照”，而不是一个可直接复用的游戏引擎或应用框架。

推荐的使用方式是：**阅读对应教程 → 运行原始示例 → 预测并修改一处代码 → 只重新构建当前示例 → 观察结果 → 对比相邻章节**。

## 1. 开始之前

你需要：

- CMake 3.14 或更高版本；
- 支持 C++17 的编译器；
- 可创建桌面图形窗口的环境；
- 支持示例所需 OpenGL 版本的显卡和驱动；
- GLFW、GLM、Assimp，以及文字渲染示例所需的 FreeType。

仓库会自行编译 GLAD 和 stb_image。`includes/` 中也带有部分头文件；`lib/` 和 `dlls/` 主要提供 Windows 预编译依赖。

项目配置本身声明的最低 CMake 版本是 3.10；本文建议 3.14+，以便直接使用 `-S` / `-B`、`--parallel` 和 Visual Studio 2019 生成器。若旧版本只是不识别 `--parallel`，省略该选项即可，不影响构建结果。

如果尚未获取仓库：

```bash
git clone https://github.com/JoeyDeVries/LearnOpenGL.git
cd LearnOpenGL
```

## 2. 五分钟运行第一个示例

下面以 macOS 为例。先安装依赖：

```bash
xcode-select --install
brew install cmake assimp glm glfw freetype
```

在仓库根目录配置工程，只构建三角形示例：

```bash
cmake -S . -B build-macos -DCMAKE_BUILD_TYPE=Debug
cmake --build build-macos \
  --target 1.getting_started__2.1.hello_triangle \
  --parallel
```

从对应章节的输出目录运行：

```bash
cd bin/1.getting_started
./1.getting_started__2.1.hello_triangle
```

看到三角形窗口即表示环境正常；按 `Esc` 退出。这个入门示例的 Shader 写在 C++ 源码中，从根目录运行也可以；为兼容后续从文件加载 Shader 的示例，建议从一开始就统一在对应的 `bin/<chapter>` 目录运行。

## 3. 分平台构建

### 3.1 macOS

安装 Xcode 命令行工具与依赖后，推荐使用单独的构建目录：

```bash
cmake -S . -B build-macos -DCMAKE_BUILD_TYPE=Debug
cmake --build build-macos --parallel
```

学习时通常只需构建当前课程：

```bash
cmake --build build-macos \
  --target 1.getting_started__7.4.camera_class \
  --parallel
```

也可以生成 Xcode 工程：

```bash
cmake -S . -B build-xcode -G Xcode
cmake --build build-xcode --config Debug
```

Apple Silicon 上 Homebrew 通常安装在 `/opt/homebrew`。macOS 系统 OpenGL 最高为 4.1；主线课程主要使用 OpenGL 3.3，可以运行，但下列 Guest 示例要求更高版本，不能通过系统 OpenGL 正常启动：

- `8.guest_2020_oit`：OpenGL 4.2；
- `8.guest_2022_5.computeshader_helloworld`：OpenGL 4.3；
- `8.guest_2021_4.dsa`：OpenGL 4.5。

### 3.2 Linux（Ubuntu/Debian）

安装编译工具和仓库所需依赖：

```bash
sudo apt update
sudo apt install g++ cmake git \
  libsoil-dev libglm-dev libassimp-dev libglew-dev libglfw3-dev \
  libxinerama-dev libxcursor-dev libxi-dev libfreetype-dev \
  libgl1-mesa-dev xorg-dev
```

配置并构建：

```bash
cmake -S . -B build -DCMAKE_BUILD_TYPE=Debug
cmake --build build --parallel
```

只构建一个示例：

```bash
cmake --build build \
  --target 1.getting_started__2.1.hello_triangle \
  --parallel
```

其他发行版请安装含义相同的开发包。Linux 下 Shader 在 CMake 配置阶段复制到 `bin/`；修改 Shader 后若输出目录仍是旧版本，再执行一次：

```bash
cmake -S . -B build
```

### 3.3 Windows

安装 CMake 和 Visual Studio，并勾选“使用 C++ 的桌面开发”工作负载。仓库自带脚本固定使用 Visual Studio 2019，可在 Git Bash 中运行：

```bash
bash build_windows.sh
```

对应的显式命令为：

```bash
cmake -S . -B out -G "Visual Studio 16 2019"
cmake --build out --config Debug
```

只构建一个示例：

```powershell
cmake --build out --config Debug --target 1.getting_started__2.1.hello_triangle
```

典型的 Debug 运行方式：

```powershell
Set-Location bin\1.getting_started\Debug
.\1.getting_started__2.1.hello_triangle.exe
```

如使用其他 Visual Studio 版本，可省略 `-G` 让 CMake 选择已安装的生成器，或指定与本机一致的生成器。切换生成器时请使用新的构建目录。

仓库中的 `.lib` 和 `.dll` 由特定工具链预编译。如果出现大量链接错误，通常是编译器版本、运行库或 x86/x64 架构不匹配；此时应使用当前工具链重新编译相应第三方库。

## 4. 仓库地图

| 路径 | 内容 | 如何使用 |
| --- | --- | --- |
| `src/1.getting_started` | 窗口、三角形、Shader、纹理、变换、坐标系、相机 | 所有初学者从这里开始 |
| `src/2.lighting` | Phong 光照、材质、光照贴图、各类光源 | 完成相机章节后顺序学习 |
| `src/3.model_loading` | Assimp、Mesh、Model | 把基础渲染扩展到复杂模型 |
| `src/4.advanced_opengl` | 深度、模板、混合、帧缓冲、Cubemap、UBO、几何着色器、实例化、MSAA | 理解渲染管线和 OpenGL 状态 |
| `src/5.advanced_lighting` | Gamma、阴影、法线/视差贴图、HDR、Bloom、延迟渲染、SSAO | 写实渲染主线 |
| `src/6.pbr` | PBR 直接光照、纹理化 PBR、IBL | 主线进阶部分 |
| `src/7.in_practice` | 调试、文字渲染、2D 游戏源码 | 综合实践 |
| `src/8.guest` | 场景图、骨骼动画、CSM、地形、Compute Shader 等专题 | 完成相应前置章节后按需学习 |
| `includes/learnopengl` | Shader、Camera、Mesh、Model、动画、文件路径等公共封装 | 示例出现抽象类型时回查 |
| `resources` | 纹理、HDR、模型、字体、关卡和音频 | 保持目录结构，不要随意移动 |
| `cmake/modules` | GLFW、GLM、Assimp 的查找脚本 | 排查 CMake 依赖问题 |
| `configuration` | 源码根目录和 Visual Studio 工作目录模板 | 由 CMake 使用 |
| `lib`、`dlls` | Windows 预编译库和运行时 DLL | Windows 构建使用 |
| `bin/<chapter>` | 可执行文件以及复制或链接后的 Shader | 从这里运行示例 |
| `build*`、`out` | CMake 生成文件 | 不是课程源码 |

## 5. 目标名称、源码和产物如何对应

主线课程的目录通常是：

```text
src/<章节>/<示例>/<入口.cpp + Shader 文件>
```

其 CMake 目标名为：

```text
<章节>__<示例>
```

例如：

```text
源码目录：src/5.advanced_lighting/7.bloom
CMake 目标：5.advanced_lighting__7.bloom
程序位置：bin/5.advanced_lighting/5.advanced_lighting__7.bloom
```

Guest 目标会把完整路径中的 `/` 替换为 `_`：

```text
源码目录：src/8.guest/2021/2.csm
CMake 目标：8.guest_2021_2.csm
程序位置：bin/8.guest/2021/2.csm/8.guest_2021_2.csm
```

常见文件后缀：

- `.cpp`：程序入口、初始化、渲染循环和输入处理；
- `.vs`：顶点着色器；
- `.fs`：片段着色器；
- `.gs`：几何着色器；
- `.tcs` / `.tes`：细分控制/细分求值着色器；
- `.cs`：计算着色器，不是 C# 文件；
- `.h`：示例局部或公共辅助代码。

使用 Makefiles 或 Ninja 生成器时，可以查看已生成的目标：

```bash
cmake --build build-macos --target help
```

将 `build-macos` 换成自己的构建目录。Visual Studio 和 Xcode 等 IDE 生成器不一定提供 `help` 目标，请在 IDE 的目标列表中查看，或直接查阅顶层 `CMakeLists.txt`。

当前顶层 `CMakeLists.txt` 注册了 92 个可执行示例，它是可构建目标的权威清单。磁盘上存在源码目录，不代表默认一定有对应目标；部分练习答案没有注册，`src/5.advanced_lighting/3.3.csm` 未列入清单，`src/7.in_practice/3.2d_game` 也默认关闭。

## 6. 推荐学习顺序

### 第一阶段：图形管线基础

按 `src/1.getting_started` 的顺序学习：

```text
hello_window → hello_triangle → shaders → textures
→ transformations → coordinate_systems → camera
```

第一遍先走非 `exercise` 示例，目标是到达 `7.4.camera_class`；第二遍再回做练习，先自己实现，再查看答案目录。

### 第二阶段：光照与模型

按下列顺序学习：

```text
2.lighting：colors → diffuse/specular → materials
           → lighting maps → light casters → multiple lights
3.model_loading：Assimp → Mesh/Model → Shader/Camera 组合
```

### 第三阶段：高级 OpenGL

建议按概念依赖推进：

```text
depth → stencil → blending → framebuffer → cubemap
→ UBO → geometry shader → instancing → MSAA
```

`7.in_practice/1.debugging` 可以提前到入门章结束后学习。

### 第四阶段：高级光照与 PBR

```text
Blinn-Phong → Gamma → Shadow Mapping → Point Shadows
→ Normal/Parallax Mapping → HDR → Bloom
→ Deferred Shading → SSAO → PBR → IBL
```

Guest 专题不必线性通读。例如骨骼动画适合放在模型加载后，CSM 放在阴影后，物理 Bloom 放在 HDR/Bloom/PBR 后。

### 时间有限时的里程碑路线

依次构建并理解这些代表性目标：

```text
1.getting_started__1.1.hello_window
1.getting_started__2.1.hello_triangle
1.getting_started__3.3.shaders_class
1.getting_started__4.2.textures_combined
1.getting_started__5.1.transformations
1.getting_started__6.3.coordinate_systems_multiple
1.getting_started__7.4.camera_class
2.lighting__2.2.basic_lighting_specular
2.lighting__6.multiple_lights
3.model_loading__1.model_loading
4.advanced_opengl__5.1.framebuffers
4.advanced_opengl__10.3.asteroids_instanced
5.advanced_lighting__3.1.3.shadow_mapping
5.advanced_lighting__7.bloom
5.advanced_lighting__8.1.deferred_shading
6.pbr__2.2.2.ibl_specular_textured
```

## 7. 每个示例怎么学

建议对每一节执行同一套小循环：

1. 阅读官网对应章节，先写下这一节新增的概念和预期画面。
2. 找到同名目录，先看入口 `.cpp` 中的初始化、渲染循环和 `processInput`。
3. 再看同目录 Shader，确认顶点属性、Uniform、输入输出以及最终颜色。
4. 只构建当前 CMake 目标，先确认未修改的基线可以运行。
5. 每次只改变一个变量，例如清屏色、顶点位置、光照参数、纹理混合比例或 Shader 公式。
6. 重建并观察差异；把“预期—结果—原因”记在自己的学习笔记中。
7. 对比相邻示例，找出这一节真正增加的代码，而不是孤立地通读整份文件。

示例有意保留大量重复代码，以便每个目录可以独立阅读。学习阶段不必急于把它们重构为统一框架。

## 8. 默认操作方式

多数可漫游示例使用：

- `Esc`：退出；
- `W` / `A` / `S` / `D`：前、左、后、右移动；
- 鼠标移动：转动相机；
- 鼠标滚轮：调整视野缩放。

部分示例还用 `Space`、`Q` / `E`、`B` 等键切换阴影、Bloom、曝光或其他参数。不同示例并不完全一致，运行前查看该 `.cpp` 文件中的 `processInput` 最可靠。

## 9. Shader、资源与工作目录

这两类路径的处理方式不同：

- Shader 常以 `"7.4.camera.vs"` 这样的相对文件名加载，依赖当前工作目录；
- 纹理、模型和字体通常通过 `FileSystem::getPath()` 相对源码根目录加载。

CMake 会把 Shader 复制或链接到程序输出目录。因此应从对应的 `bin/<chapter>` 目录启动；Windows 多配置构建通常还要进入其 `Debug` 或 `Release` 子目录。

如果移动了仓库，旧程序中编译时记录的源码绝对路径会失效。首选做法是重新配置和构建；也可在运行前覆盖资源根目录：

```bash
export LOGL_ROOT_PATH=/absolute/path/to/LearnOpenGL
```

PowerShell：

```powershell
$env:LOGL_ROOT_PATH = "C:\absolute\path\to\LearnOpenGL"
```

该目录下应能看到 `README.md` 和 `resources/`。注意：`LOGL_ROOT_PATH` 只解决纹理、模型、字体等资源路径，不能代替正确的 Shader 工作目录。

## 10. 常见问题

### CMake 找不到 GLFW、GLM、Assimp 或 FreeType

先确认安装的是带头文件和库文件的开发包，再使用一个新的构建目录重新配置。若依赖安装在非标准位置，可给 CMake 提供相应根目录或具体 include/library 路径。

### `No rule to make target` 或目标不存在

先重新运行 CMake 配置。Makefiles 或 Ninja 生成器可用 `--target help` 查询；Visual Studio 和 Xcode 请查看 IDE 目标列表。仍找不到时检查顶层 `CMakeLists.txt`；若目录没有出现在章节列表中，就不会生成目标。

### 出现 `ERROR::SHADER::FILE_NOT_SUCCESSFULLY_READ`

进入该示例对应的 `bin/<chapter>` 目录后再运行。还应确认构建步骤已把 `.vs`、`.fs` 等文件复制或链接到程序旁边。

### 纹理、模型或字体加载失败

确认 `resources/` 完整存在。仓库移动后重新执行 CMake 配置，或正确设置 `LOGL_ROOT_PATH`。

### 程序启动后立即退出

从终端运行，不要双击程序，这样才能看到 GLFW、GLAD、Shader 或资源加载错误。优先处理终端输出的第一条错误。

### 窗口黑屏或 OpenGL 上下文创建失败

检查显卡驱动和示例请求的 OpenGL 版本。macOS 尤其要注意 4.1 的系统上限；远程终端、容器或无桌面环境通常也无法直接创建 GLFW 窗口。

### Windows 出现大量 unresolved external symbols

检查构建架构和运行库是否与 `lib/` 中的预编译库一致。若工具链不同，重新编译第三方依赖通常比逐个修补链接参数更可靠。

### 文字渲染说明与当前代码不一致

`src/7.in_practice/2.text_rendering/readme.md` 是旧说明。当前顶层 CMake 已启用 `7.in_practice__2.text_rendering`，代码使用仓库内的 `resources/fonts/Antonio-Bold.ttf`；无需按旧说明取消注释目标或复制 Arial 字体。

### 为什么没有自动测试结果

仓库没有自动化测试套件。一次学习修改的基本验证是：当前目标成功构建、程序能从正确目录启动、终端没有资源或 Shader 错误、画面和交互符合预期。

## 11. 修改或增加个人实验

最轻量的做法是直接修改当前示例，并用 Git 查看差异：

```bash
git diff -- src/1.getting_started/2.1.hello_triangle
```

如果希望保留原示例，可复制一个最接近的示例目录，在顶层 `CMakeLists.txt` 对应章节列表中加入新目录名，然后重新运行 CMake 配置。普通目标会按 `<章节>__<目录名>` 自动生成。

复制目录时还要为其中的 Shader 使用新文件名，并同步更新 `.cpp` 中的 `Shader(...)` 路径。同一章节的所有程序共享 `bin/<chapter>`；沿用原 Shader 名会导致不同目标互相覆盖文件或符号链接。

请只编辑 `src/`、`includes/learnopengl/`、`resources/` 或构建配置中的必要文件，不要把 `bin/`、`build*`、`out/` 当作源文件目录。

## 12. 发布模式、清理与验证

单配置生成器（常见于 Makefiles/Ninja）的 Release 构建：

```bash
cmake -S . -B build-release -DCMAKE_BUILD_TYPE=Release
cmake --build build-release --target 1.getting_started__2.1.hello_triangle --parallel
```

Visual Studio 多配置生成器使用：

```bash
cmake --build out --config Release --target 1.getting_started__2.1.hello_triangle
```

Xcode 使用其实际构建目录：

```bash
cmake --build build-xcode --config Release --target 1.getting_started__2.1.hello_triangle
```

清理当前构建目录中的产物：

```bash
cmake --build build-macos --target clean
```

切换编译器、架构或生成器时，使用新的构建目录通常更省事，也不会影响课程源码。

## 13. 许可

除非另有说明，代码示例采用 [CC BY-NC 4.0](LICENSE.md)，并非 MIT 许可。引用、再发布或改编时应保留署名，商业用途需要额外确认。

字体、模型、纹理等资源可能有各自的许可或来源说明。发布包含这些素材的作品前，请同时检查 `resources/` 下对应的 `LICENSE`、字体许可和来源文件。
