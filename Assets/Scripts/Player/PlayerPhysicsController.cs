using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerPhysicsController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Camera _camera;
    private Rigidbody _rigidBody;
    private float _xMove;
    private float _zMove;
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
        _xMove = Input.GetAxis("Horizontal");
        _zMove = Input.GetAxis("Vertical");
        _directionMove = new Vector3(_xMove, 0, _zMove).normalized;
    }

    private void Locomotion()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        if (!groundPlane.Raycast(ray, out float enter)) return;

        Vector3 mousePosition = ray.GetPoint(enter);
        Vector3 direction = mousePosition - transform.position;
        direction.y = 0;

        if (direction.magnitude > 0)
        {
            Vector3 moveDirection = direction.normalized;
            _rigidBody.MovePosition(_rigidBody.position + moveDirection * _speed * Time.fixedDeltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
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
