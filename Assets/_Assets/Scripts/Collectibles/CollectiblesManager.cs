using UnityEngine;

namespace Collectibles
{
    public class CollectiblesManager : MonoBehaviour
    {
        [SerializeField] private Vector3 _collectiblePosition;
        [SerializeField] private GameObject _collectblePrefab;

        private GameObject _currentCollectible;
        private CollectibleController _currentCollectibleController;

        private void InstantiateCollectible()
        {
            _currentCollectible = Instantiate(_collectblePrefab, _collectiblePosition, Quaternion.identity);

            _currentCollectibleController = _currentCollectible.GetComponent<CollectibleController>();

            _currentCollectibleController.OnCollectibleTrigger += HandleObjectCollection;
        }

        private void HandleObjectCollection()
        {
            _currentCollectibleController.DestroyCollectible();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                InstantiateCollectible();
            }
        }
    }
}