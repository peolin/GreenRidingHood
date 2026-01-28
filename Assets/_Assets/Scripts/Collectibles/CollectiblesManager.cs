using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collectibles
{
    public class CollectiblesManager : MonoBehaviour
    {
        //[SerializeField] private AudioSource _audioSource;

        private CollectibleController _collectibleController;

        public static event Action<string> OnObjectCollection;
        public static event Action PlayerFreezeRequested;

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
            if (_collectibleController == _triggeredController) return;

            //_audioSource.Play();
            AudioManager.Instance.PlayCollectibleAudio();

            _collectibleController = _triggeredController;

            OnObjectCollection?.Invoke("Collected this object!"); // get data from collectible
            PlayerFreezeRequested?.Invoke();

            TextPanelManager.OnCollectibleTextHidden += DestroyCollectedObject;
        }

        private void DestroyCollectedObject()
        {
            TextPanelManager.OnCollectibleTextHidden -= DestroyCollectedObject;
            _collectibleController.DestroyCollectible();

            _collectibleController = null;
        }
    }
}