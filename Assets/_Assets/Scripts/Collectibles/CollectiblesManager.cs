using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collectibles
{
    public class CollectiblesManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private CollectibleController _collectibleController;

        private void OnEnable()
        {
            CollectibleController.OnCollectibleTrigger += HandleObjectCollection;
        }

        private void OnDisable()
        {
            CollectibleController.OnCollectibleTrigger -= HandleObjectCollection;
        }

        private void HandleObjectCollection(CollectibleController _triggeredController)
        {
            _audioSource.Play();

            _collectibleController = _triggeredController;

            StartCoroutine(DestroyCollectedObject());
        }

        private IEnumerator DestroyCollectedObject()
        {
            yield return new WaitForSeconds(1);
            _collectibleController.DestroyCollectible();
            _collectibleController = null;
        }
    }
}