using UnityEngine;

public class CameraPivotRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 3f;   // чувствительность мыши
    [SerializeField] private float rotationSmooth = 0.1f; // сглаживание поворота

    private float yaw = 0f;
    private float pitch = 20f;

    private void LateUpdate()
    {
        // Вращение мышью
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, 5f, 80f);

        // Плавный поворот Pivot
        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0f);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rotationSmooth);
    }
}
