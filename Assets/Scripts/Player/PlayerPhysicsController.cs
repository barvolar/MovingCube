using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Camera mainCamera;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (mainCamera == null) mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
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

            Vector3 moveDir = (inputDir.x * camRight + inputDir.z * camForward).normalized;

            Vector3 targetPos = rb.position + moveDir * speed * Time.fixedDeltaTime;
            rb.MovePosition(Vector3.Lerp(rb.position, targetPos, 0.5f));
        }
    }
}
