using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerPhysicsController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Camera _camera;
    private Rigidbody _rigidBody;
    private float _horizontal;
    private float _vertical;
    private Vector3 _directionMove;
    private bool _isGrounded;

    private void Awake()
    {
        if (_camera == null)
            _camera = Camera.main;

        if (_rigidBody == null)
            _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CalculateValue();
        Jump();
    }

    private void FixedUpdate()
    {
        Locomotion();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal == Vector3.up)
        {
            _isGrounded = true;
        }
    }

    private void CalculateValue()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _directionMove = new Vector3(_horizontal, 0, _vertical).normalized;
    }

    private void Locomotion()
    {
        Vector3 cameraForvard = _camera.transform.forward;
        cameraForvard.y = 0;
        cameraForvard.Normalize();

        Vector3 cameraRight = _camera.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        Vector3 direction = cameraForvard * _vertical + cameraRight * _horizontal;

        if (direction.magnitude > 0.1f)
        {
            direction.Normalize();

            _rigidBody.MovePosition(_rigidBody.position + direction * _speed * Time.fixedDeltaTime);
            
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _rigidBody.MoveRotation(Quaternion.Slerp(_rigidBody.rotation, targetRotation, 0.2f));
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }
    }


}
