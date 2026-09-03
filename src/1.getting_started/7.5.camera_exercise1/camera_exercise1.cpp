// LearnOpenGL 中文导读
// 文件性质：这是 7.4 Camera::ProcessKeyboard 的练习答案片段，不是独立 C++ 程序。
// 与基础示例的精确差异：四向移动计算后强制 Position.y=0，使相机始终停留在世界 XZ 地面平面。
// 观察重点：MovementSpeed 标量不变；清零 y 后，前后地面位移长度为 velocity*|cos(Pitch)|，Pitch 越陡就越慢。

// This function is found in the camera class. What we basically do is keep the y position value at 0.0f to force our
// user to stick to the ground.

[...]
// processes input received from any keyboard-like input system. Accepts input parameter in the form of camera defined ENUM (to abstract it from windowing systems)
void ProcessKeyboard(Camera_Movement direction, float deltaTime)
{
    // deltaTime 先把每秒速度换算为本帧位移，四个方向更新完成后再统一锁定高度。
    float velocity = MovementSpeed * deltaTime;
    if (direction == FORWARD)
        Position += Front * velocity;
    if (direction == BACKWARD)
        Position -= Front * velocity;
    if (direction == LEFT)
        Position -= Right * velocity;
    if (direction == RIGHT)
        Position += Right * velocity;
    // make sure the user stays at the ground level
    // 关键差异：无论 Front/Right 是否含 y 分量，最终都把相机投影回 y=0 的 XZ 平面。
    Position.y = 0.0f; // <-- this one-liner keeps the user at the ground level (xz plane)
}
[...]