using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;         // Игрок
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -7);
    [SerializeField] private float followSpeed = 5f;   // Плавность движения
    [SerializeField] private float rotationSpeed = 3f; // Чувствительность мыши

    private float yaw = 0f;   // Горизонтальное вращение
    private float pitch = 20f; // Вертикальное вращение

    private void LateUpdate()
    {
        if (target == null) return;

        // --- Вращение мышью ---
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, 5f, 80f);

        // --- Позиция камеры ---
        Vector3 desiredPosition = target.position + Quaternion.Euler(pitch, yaw, 0) * offset;

        // Если игрок двигается через Rigidbody → используем Time.fixedDeltaTime для синхронизации
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.fixedDeltaTime);

        // --- Камера смотрит на игрока с небольшим смещением по Y ---
        Vector3 lookTarget = target.position + Vector3.up * 1.5f;
        transform.LookAt(lookTarget);
    }
}
