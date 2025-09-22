using UnityEngine;

namespace Collectibles
{
    public class CollectiblesSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _entityToSpawn;
        [SerializeField] private SpawnManagerScriptableObject _collectiblesSpawnerValues;

        private int _intstanceNumber = 1;

        private void Start()
        {
            SpawnEntities();
        }

        private void SpawnEntities()
        {
            int currentSpawnPointIndex = 0;

            for (int i = 0; i < _collectiblesSpawnerValues.numberOfPrefabsToCreate; i++)
            {
                GameObject currentEntity = Instantiate(_entityToSpawn, _collectiblesSpawnerValues.spawnPoints[currentSpawnPointIndex], Quaternion.identity);

                currentEntity.name = _collectiblesSpawnerValues.prefabName + _intstanceNumber;

                currentSpawnPointIndex = (currentSpawnPointIndex + 1) % _collectiblesSpawnerValues.spawnPoints.Length;

                _intstanceNumber++;
            }
        }
    }
}
