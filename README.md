# learnopengl.com code repository
Contains code samples for all chapters of Learn OpenGL and [https://learnopengl.com](https://learnopengl.com). 

## Windows building
All relevant libraries are found in /libs and all DLLs found in /dlls (pre-)compiled for Windows. 
The CMake script knows where to find the libraries so just run CMake script and generate project of choice.

Keep in mind the supplied libraries were generated with a specific compiler version which may or may not work on your system (generating a large batch of link errors). In that case it's advised to build the libraries yourself from the source.

## Linux building
First make sure you have CMake, Git, and GCC by typing as root (sudo) `apt-get install g++ cmake git` and then get the required packages:
Using root (sudo) and type `apt-get install libsoil-dev libglm-dev libassimp-dev libglew-dev libglfw3-dev libxinerama-dev libxcursor-dev  libxi-dev libfreetype-dev libgl1-mesa-dev xorg-dev` .

**Build through CMake-gui:** The source directory is LearnOpenGL and specify the build directory as LearnOpenGL/build. Creating the build directory within LearnOpenGL is important for linking to the resource files (it also will be ignored by Git). Hit configure and specify your compiler files (Unix Makefiles are recommended), resolve any missing directories or libraries, and then hit generate. Navigate to the build directory (`cd LearnOpenGL/build`) and type `make` in the terminal. This should generate the executables in the respective chapter folders.

**Build through Cmake command line:**
```
cd /path/to/LearnOpenGL
mkdir build && cd build
cmake ..
cmake --build .
```

Note that CodeBlocks or other IDEs may have issues running the programs due to problems finding the shader and resource files, however it should still be able to generate the executables. To work around this problem it is possible to set an environment variable to tell the tutorials where the resource files can be found. The environment variable is named LOGL_ROOT_PATH and may be set to the path to the root of the LearnOpenGL directory tree. For example:

    `export LOGL_ROOT_PATH=/home/user/tutorials/LearnOpenGL`

Running `ls $LOGL_ROOT_PATH` should list, among other things, this README file and the resources directory.

## Mac OS X building
Install the Xcode command-line tools and the Homebrew dependencies, then configure a separate build directory:
```
xcode-select --install
brew install cmake assimp glm glfw freetype
cmake -S . -B build-macos -DCMAKE_BUILD_TYPE=Debug
cmake --build build-macos --parallel $(sysctl -n hw.logicalcpu)
```

To build only one lesson while learning:
```
cmake --build build-macos --target 1.getting_started__2.1.hello_triangle --parallel $(sysctl -n hw.logicalcpu)
```

Executables and shader symlinks are generated under `bin/<chapter>`. Run an executable from its chapter directory so relative shader paths resolve correctly:
```
cd bin/1.getting_started
./1.getting_started__2.1.hello_triangle
```

Apple Silicon Homebrew installs the required arm64 libraries under `/opt/homebrew`; CMake discovers them automatically. macOS provides OpenGL up to version 4.1. The main lessons use OpenGL 3.3 and are supported, but guest examples that explicitly require newer contexts—weighted blended OIT (4.2), compute shaders (4.3), and DSA (4.5)—cannot run with Apple's system OpenGL implementation even though their C++ sources can compile.

## Create Xcode project on Mac platform
Thanks [@caochao](https://github.com/caochao):
After cloning the repo, go to the root path of the repo, and run the command below:
```
mkdir xcode
cd xcode
cmake -G Xcode ..
```

## Glitter
Polytonic created a project called [Glitter](https://github.com/Polytonic/Glitter) that is a dead-simple boilerplate for OpenGL. 
Everything you need to run a single LearnOpenGL Project (including all libraries) and just that; nothing more. 
Perfect if you want to follow along with the chapters, without the hassle of having to manually compile and link all third party libraries!

## Ports
Check out [@srcres258](https://github.com/srcres258)'s port in Rust [here](https://github.com/srcres258/learnopengl-rust/).
