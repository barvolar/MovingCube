using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private ParticleSystem _collectibleEffectPrefab;
    [SerializeField] private int _valuePoint;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            if (_collectibleEffectPrefab != null)
                Instantiate(_collectibleEffectPrefab, transform.position, Quaternion.identity);

            ScoreManager.Instance.AddScore(_valuePoint);
            Destroy(gameObject);
        }
    }
}
