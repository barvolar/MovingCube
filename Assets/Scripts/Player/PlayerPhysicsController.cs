using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerSmoothMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSmooth = 0.15f; // сглаживание поворота
    [SerializeField] private Camera mainCamera;

    private Rigidbody rb;
    private Vector3 moveDirection = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (mainCamera == null) mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        // Получаем ввод (WASD)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(h, 0, v).normalized;

        if (inputDir.magnitude >= 0.1f)
        {
            // Направление относительно камеры
            Vector3 camForward = mainCamera.transform.forward;
            camForward.y = 0;
            Vector3 camRight = mainCamera.transform.right;
            camRight.y = 0;

            moveDirection = (inputDir.x * camRight + inputDir.z * camForward).normalized;

            // Плавное движение
            Vector3 targetPos = rb.position + moveDirection * speed * Time.fixedDeltaTime;
            rb.MovePosition(Vector3.Lerp(rb.position, targetPos, 0.5f));

            // Плавный поворот
            Quaternion targetRot = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, rotationSmooth));
        }
    }
}
