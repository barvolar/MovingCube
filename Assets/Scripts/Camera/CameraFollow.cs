using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;        // Игрок
    [SerializeField] private float followSpeed = 5f;  // Плавность движения Pivot
    [SerializeField] private float rotationSpeed = 3f; // Чувствительность мыши
    [SerializeField] private float rotationSmooth = 0.1f; // сглаживание вращения

    private float yaw = 0f;
    private float pitch = 20f;

    private void LateUpdate()
    {
        if (target == null) return;

        // --- Вращение мышью ---
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, 5f, 80f);

        // --- Плавное движение Pivot к игроку ---
        Vector3 desiredPosition = target.position;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // --- Плавный поворот Pivot ---
        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmooth);
    }
}
