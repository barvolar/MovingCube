using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offsed;
    [SerializeField] private float _smoothSpeed = 0.125f;

    private Vector3 _position;

    private void LateUpdate()
    {
        CalculateValue();
    }

    private void CalculateValue()
    {
        Vector3 _targetPosition = _target.position + _offsed;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, _targetPosition, _smoothSpeed);

        transform.position = smoothPosition;
        transform.LookAt(_target);
    }
}
