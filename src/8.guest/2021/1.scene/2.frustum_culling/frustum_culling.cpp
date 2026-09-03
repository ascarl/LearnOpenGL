// LearnOpenGL 中文导读
// 学习目标：在场景图遍历阶段用相机六个视锥平面剔除不可见实体，减少提交给 GPU 的模型数量。
// 核心流程：CPU 从相机位置、方向、FOV、宽高比和近远面构建 Frustum，再递归测试实体包围体并只绘制相交节点。
// 观察重点：total 统计 CPU 访问节点数，display 统计通过剔除并提交 GPU 的节点数，可直接比较剔除收益。

#include <glad/glad.h>
#include <GLFW/glfw3.h>

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

#include <learnopengl/filesystem.h>
#include <learnopengl/shader_m.h>
#include <learnopengl/camera.h>
#include <learnopengl/model.h>
#include <learnopengl/entity.h>

#ifndef ENTITY_H
#define ENTITY_H

#include <list> //std::list
#include <memory> //std::unique_ptr

class Entity : public Model
{
public:
	list<unique_ptr<Entity>> children;
	Entity* parent;
};
#endif


#include <iostream>

void framebuffer_size_callback(GLFWwindow* window, int width, int height);
void mouse_callback(GLFWwindow* window, double xpos, double ypos);
void scroll_callback(GLFWwindow* window, double xoffset, double yoffset);
void processInput(GLFWwindow* window);

// settings
const unsigned int SCR_WIDTH = 800;
const unsigned int SCR_HEIGHT = 600;

// camera
Camera camera(glm::vec3(0.0f, 10.0f, 0.0f));
Camera cameraSpy(glm::vec3(0.0f, 10.0f, 0.f));
float lastX = SCR_WIDTH / 2.0f;
float lastY = SCR_HEIGHT / 2.0f;
bool firstMouse = true;

// timing
float deltaTime = 0.0f;
float lastFrame = 0.0f;

int main()
{
	// glfw: initialize and configure
	// ------------------------------
	glfwInit();
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

#ifdef __APPLE__
	glfwWindowHint(GLFW_OPENGL_FORWARD_COMPAT, GL_TRUE);
#endif

	// glfw window creation
	// --------------------
	GLFWwindow* window = glfwCreateWindow(SCR_WIDTH, SCR_HEIGHT, "LearnOpenGL", NULL, NULL);
	if (window == NULL)
	{
		std::cout << "Failed to create GLFW window" << std::endl;
		glfwTerminate();
		return -1;
	}
	glfwMakeContextCurrent(window);
	glfwSetFramebufferSizeCallback(window, framebuffer_size_callback);
	glfwSetCursorPosCallback(window, mouse_callback);
	glfwSetScrollCallback(window, scroll_callback);

	// tell GLFW to capture our mouse
	glfwSetInputMode(window, GLFW_CURSOR, GLFW_CURSOR_DISABLED);

	// glad: load all OpenGL function pointers
	// ---------------------------------------
	if (!gladLoadGLLoader((GLADloadproc)glfwGetProcAddress))
	{
		std::cout << "Failed to initialize GLAD" << std::endl;
		return -1;
	}

	// tell stb_image.h to flip loaded texture's on the y-axis (before loading model).
	stbi_set_flip_vertically_on_load(true);

	// configure global opengl state
	// -----------------------------
	glEnable(GL_DEPTH_TEST);

	camera.MovementSpeed = 20.f;

	// build and compile shaders
	// -------------------------
	Shader ourShader("1.model_loading.vs", "1.model_loading.fs");

	// load entities
	// -----------
	Model model(FileSystem::getPath("resources/objects/planet/planet.obj"));
	Entity ourEntity(model);
	ourEntity.transform.setLocalPosition({ 0, 0, 0 });
	const float scale = 1.0;
	ourEntity.transform.setLocalScale({ scale, scale, scale });

	{
		Entity* lastEntity = &ourEntity;

		// 关键步骤：在 XZ 平面生成 20x20 个实体，提供足够多的视锥内外候选对象用于对比。
		for (unsigned int x = 0; x < 20; ++x)
		{
			for (unsigned int z = 0; z < 20; ++z)
			{
				ourEntity.addChild(model);
				lastEntity = ourEntity.children.back().get();

				//Set transform values
				lastEntity->transform.setLocalPosition({ x * 10.f - 100.f,  0.f, z * 10.f - 100.f });
			}
		}
	}
	ourEntity.updateSelfAndChild();

	// draw in wireframe
	//glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);

	// render loop
	// -----------
	while (!glfwWindowShouldClose(window))
	{
		// per-frame time logic
		// --------------------
		float currentFrame = glfwGetTime();
		deltaTime = currentFrame - lastFrame;
		lastFrame = currentFrame;

		// input
		// -----
		processInput(window);

		// render
		// ------
		glClearColor(0.05f, 0.05f, 0.05f, 1.0f);
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

		// don't forget to enable shader before setting uniforms
		ourShader.use();

		// view/projection transformations
		glm::mat4 projection = glm::perspective(glm::radians(camera.Zoom), (float)SCR_WIDTH / (float)SCR_HEIGHT, 0.1f, 100.0f);
		// Frustum 与本帧投影参数保持一致；六个平面都在世界空间，便于和实体世界包围体测试。
		const Frustum camFrustum = createFrustumFromCamera(camera, (float)SCR_WIDTH / (float)SCR_HEIGHT, glm::radians(camera.Zoom), 0.1f, 100.0f);

		cameraSpy.ProcessMouseMovement(2, 0);
		//static float acc = 0;
		//acc += deltaTime * 0.0001;
		//cameraSpy.Position = { cos(acc) * 10, 0.f, sin(acc) * 10 };
		glm::mat4 view = camera.GetViewMatrix();

		ourShader.setMat4("projection", projection);
		ourShader.setMat4("view", view);

		// draw our scene graph
		unsigned int total = 0, display = 0;
		// 数据流：递归访问场景图，包围体完全位于任一平面外则跳过 Draw；相交或在内部才提交网格。
		ourEntity.drawSelfAndChild(camFrustum, ourShader, display, total);
		std::cout << "Total process in CPU : " << total << " / Total send to GPU : " << display << std::endl;

		//ourEntity.transform.setLocalRotation({ 0.f, ourEntity.transform.getLocalRotation().y + 20 * deltaTime, 0.f });
		ourEntity.updateSelfAndChild();

		// glfw: swap buffers and poll IO events (keys pressed/released, mouse moved etc.)
		// -------------------------------------------------------------------------------
		glfwSwapBuffers(window);
		glfwPollEvents();
	}

	// glfw: terminate, clearing all previously allocated GLFW resources.
	// ------------------------------------------------------------------
	glfwTerminate();
	return 0;
}

// process all input: query GLFW whether relevant keys are pressed/released this frame and react accordingly
// ---------------------------------------------------------------------------------------------------------
void processInput(GLFWwindow* window)
{
	if (glfwGetKey(window, GLFW_KEY_ESCAPE) == GLFW_PRESS)
		glfwSetWindowShouldClose(window, true);

	if (glfwGetKey(window, GLFW_KEY_W) == GLFW_PRESS)
		camera.ProcessKeyboard(FORWARD, deltaTime);
	if (glfwGetKey(window, GLFW_KEY_S) == GLFW_PRESS)
		camera.ProcessKeyboard(BACKWARD, deltaTime);
	if (glfwGetKey(window, GLFW_KEY_A) == GLFW_PRESS)
		camera.ProcessKeyboard(LEFT, deltaTime);
	if (glfwGetKey(window, GLFW_KEY_D) == GLFW_PRESS)
		camera.ProcessKeyboard(RIGHT, deltaTime);
}

// glfw: whenever the window size changed (by OS or user resize) this callback function executes
// ---------------------------------------------------------------------------------------------
void framebuffer_size_callback(GLFWwindow* window, int width, int height)
{
	// make sure the viewport matches the new window dimensions; note that width and 
	// height will be significantly larger than specified on retina displays.
	glViewport(0, 0, width, height);
}

// glfw: whenever the mouse moves, this callback is called
// -------------------------------------------------------
void mouse_callback(GLFWwindow* window, double xpos, double ypos)
{
	if (firstMouse)
	{
		lastX = xpos;
		lastY = ypos;
		firstMouse = false;
	}

	float xoffset = xpos - lastX;
	float yoffset = lastY - ypos; // reversed since y-coordinates go from bottom to top

	lastX = xpos;
	lastY = ypos;

	camera.ProcessMouseMovement(xoffset, yoffset);
}

// glfw: whenever the mouse scroll wheel scrolls, this callback is called
// ----------------------------------------------------------------------
void scroll_callback(GLFWwindow* window, double xoffset, double yoffset)
{
	camera.ProcessMouseScroll(yoffset);
}
