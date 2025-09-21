using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Collectible _coinPrefab;
    [SerializeField] private float _spawnSpeed;
    [SerializeField] private Vector3 _spawnMaximumPosition;
    [SerializeField] private Vector3 _spawnMinumimPosition;

    private float _timeSpawn;
    private Vector3 _spawnPosition;
    private void Update()
    {
        _timeSpawn += Time.deltaTime;
        Spawn();
    }

    private void Spawn()
    {
        if (_timeSpawn > _spawnSpeed)
        {
            Instantiate(_coinPrefab, CalculateSpawnPosition(), Quaternion.identity);
            _timeSpawn = 0;
        }
    }

    private Vector3 CalculateSpawnPosition()
    {
        _spawnPosition = new Vector3
          ( Random.Range(_spawnMinumimPosition.x, _spawnMaximumPosition.x),
            Random.Range(_spawnMinumimPosition.y, _spawnMaximumPosition.y),
            Random.Range(_spawnMaximumPosition.z, _spawnMaximumPosition.z));

        return _spawnPosition;
    }
}
