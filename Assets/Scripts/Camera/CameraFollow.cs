using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offsed;
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private float _mouseSensetivity = 3f;

    private float _horizontalRotation;
    private float _verticalRotation;

    private void LateUpdate()
    {
        CalculateValue();
        Rotation();
    }

    private void CalculateValue()
    {
        Vector3 _targetPosition = _target.position + Quaternion.Euler(_verticalRotation, _horizontalRotation, 0) * _offsed;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, _targetPosition, _smoothSpeed);

        transform.position = smoothPosition;
        transform.LookAt(_target);
    }

    private void Rotation()
    {
        _horizontalRotation += Input.GetAxis("Mouse X") * _mouseSensetivity;
        _verticalRotation -= Input.GetAxis("Mouse Y") * _mouseSensetivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, 5, 80);
    }
}
