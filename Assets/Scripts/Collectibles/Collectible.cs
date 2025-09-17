using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int _valuePoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore(_valuePoint);
            Destroy(gameObject);
        }
    }
}
