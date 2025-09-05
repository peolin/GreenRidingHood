using UnityEngine;

namespace Collectibles
{
    public class CollectiblesSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _entityToSpawn;
        [SerializeField] private SpawnManagerScriptableObject _spawnManagerValues;

        private int _intstanceNumber = 1;

        private void Start()
        {
            SpawnEntities();
        }

        private void SpawnEntities()
        {
            int currentSpawnPointIndex = 0;

            for (int i = 0; i < _spawnManagerValues.numberOfPrefabsToCreate; i++)
            {
                GameObject currentEntity = Instantiate(_entityToSpawn, _spawnManagerValues.spawnPoints[currentSpawnPointIndex], Quaternion.identity);

                currentEntity.name = _spawnManagerValues.prefabName + _intstanceNumber;

                currentSpawnPointIndex = (currentSpawnPointIndex + 1) % _spawnManagerValues.spawnPoints.Length;

                _intstanceNumber++;
            }
        }
    }
}
