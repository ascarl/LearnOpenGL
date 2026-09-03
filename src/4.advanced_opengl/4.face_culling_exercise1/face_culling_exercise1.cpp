// LearnOpenGL 中文导读
// 学习目标：通过统一的顺时针顶点绕序定义立方体正面，为背面剔除准备可判定的三角形朝向。
// 核心流程：每个面由两个三角形组成，顶点顺序从物体外侧观察均为顺时针；配合 GL_CULL_FACE 可跳过背面光栅化。
// 观察重点：OpenGL 默认把逆时针视为正面，因此使用本练习数据时还需调用 glFrontFace(GL_CW) 改变正面约定。

float vertices[] = {
    // back face
    -0.5f, -0.5f, -0.5f,  0.0f, 0.0f, // bottom-left
     0.5f, -0.5f, -0.5f,  1.0f, 0.0f, // bottom-right    
     0.5f,  0.5f, -0.5f,  1.0f, 1.0f, // top-right              
     0.5f,  0.5f, -0.5f,  1.0f, 1.0f, // top-right
    -0.5f,  0.5f, -0.5f,  0.0f, 1.0f, // top-left
    -0.5f, -0.5f, -0.5f,  0.0f, 0.0f, // bottom-left                
    // front face
    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f, // bottom-left
     0.5f,  0.5f,  0.5f,  1.0f, 1.0f, // top-right
     0.5f, -0.5f,  0.5f,  1.0f, 0.0f, // bottom-right        
     0.5f,  0.5f,  0.5f,  1.0f, 1.0f, // top-right
    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f, // bottom-left
    -0.5f,  0.5f,  0.5f,  0.0f, 1.0f, // top-left        
    // left face
    -0.5f,  0.5f,  0.5f,  1.0f, 0.0f, // top-right
    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f, // bottom-left
    -0.5f,  0.5f, -0.5f,  1.0f, 1.0f, // top-left       
    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f, // bottom-left
    -0.5f,  0.5f,  0.5f,  1.0f, 0.0f, // top-right
    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f, // bottom-right
    // right face
     0.5f,  0.5f,  0.5f,  1.0f, 0.0f, // top-left
     0.5f,  0.5f, -0.5f,  1.0f, 1.0f, // top-right      
     0.5f, -0.5f, -0.5f,  0.0f, 1.0f, // bottom-right          
     0.5f, -0.5f, -0.5f,  0.0f, 1.0f, // bottom-right
     0.5f, -0.5f,  0.5f,  0.0f, 0.0f, // bottom-left
     0.5f,  0.5f,  0.5f,  1.0f, 0.0f, // top-left
    // bottom face          
    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f, // top-right
     0.5f, -0.5f,  0.5f,  1.0f, 0.0f, // bottom-left
     0.5f, -0.5f, -0.5f,  1.0f, 1.0f, // top-left        
     0.5f, -0.5f,  0.5f,  1.0f, 0.0f, // bottom-left
    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f, // top-right
    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f, // bottom-right
    // top face
    -0.5f,  0.5f, -0.5f,  0.0f, 1.0f, // top-left
     0.5f,  0.5f, -0.5f,  1.0f, 1.0f, // top-right
     0.5f,  0.5f,  0.5f,  1.0f, 0.0f, // bottom-right                 
     0.5f,  0.5f,  0.5f,  1.0f, 0.0f, // bottom-right
    -0.5f,  0.5f,  0.5f,  0.0f, 0.0f, // bottom-left  
    -0.5f,  0.5f, -0.5f,  0.0f, 1.0f  // top-left              
};

/* Also make sure to add a call to OpenGL to specify that triangles defined by a clockwise ordering 
   are now 'front-facing' triangles so the cube is rendered as normal:
   glFrontFace(GL_CW);
*/