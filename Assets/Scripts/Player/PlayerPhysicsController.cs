using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerPhysicsController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Rigidbody _rigidBody;
    private float _xMove;
    private float _zMove;
    private Vector3 _directionMove;
    private bool _isGrounded;

    private void Start()
    {
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
        _rigidBody.MovePosition(transform.position + _directionMove * _speed * Time.fixedDeltaTime);
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
