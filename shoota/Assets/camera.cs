using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;  // Игрок (например, "im")
    public Vector3 offset = new Vector3(0, 5f, -10f);  // Смещение камеры относительно игрока
    public float smoothSpeed = 5f;  // Плавность движения камеры
    public float rotationX = -10f;  // Угол наклона камеры вниз (по оси X)

    private void LateUpdate()
    {
        if (player == null) return;

        // Расчёт целевой позиции камеры
        Vector3 desiredPosition = player.position + offset;  // Камера будет позади и чуть выше
        desiredPosition.y = player.position.y + offset.y;  // Камера на одном уровне по Y с игроком

        // Плавное движение камеры в целевую позицию
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Камера смотрит на игрока
        transform.LookAt(player);

        // Поворот камеры вокруг игрока (по оси Y)
        transform.rotation = Quaternion.Euler(rotationX, player.eulerAngles.y, 0);  // Наклон по оси X + поворот по оси Y
    }
}
