using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;        // Игрок
    [SerializeField] private Camera cam;              // Вложенная камера
    [SerializeField] private float followSmooth = 0.1f;   // сглаживание позиции
    [SerializeField] private float rotationSmooth = 0.1f; // сглаживание вращения
    [SerializeField] private float rotationSpeed = 3f;

    private Vector3 velocity = Vector3.zero;
    private float yaw = 0f;
    private float pitch = 20f;

    private void LateUpdate()
    {
        if (target == null) return;

        // --- Плавное вращение Pivot мышью ---
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, 5f, 80f);

        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmooth);

        // --- Плавное движение Pivot за кубом ---
        Vector3 desiredPosition = target.position;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, followSmooth);
    }
}
