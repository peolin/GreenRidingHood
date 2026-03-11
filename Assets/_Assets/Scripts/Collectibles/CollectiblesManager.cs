using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collectibles
{
    public class CollectiblesManager : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;

        private CollectibleController _collectibleController;

        public event Action OnObjectCollection;
        public static event Action PlayerFreezeRequested;

        private void OnEnable()
        {
            CollectibleController.OnCollectibleTrigger += HandleObjectCollection;
        }

        private void OnDisable()
        {
            CollectibleController.OnCollectibleTrigger -= HandleObjectCollection;
        }

        private void HandleObjectCollection(CollectibleController triggeredController)
        {
            if (_collectibleController == triggeredController) return;

            AudioManager.Instance.PlayCollectibleAudio();

            _collectibleController = triggeredController;

            OnObjectCollection?.Invoke();
            PlayerFreezeRequested?.Invoke();

            _uiManager.OnUIInteractionEnded += DestroyCollectedObject;
        }

        private void DestroyCollectedObject() // shift to using a pool
        {
            _uiManager.OnUIInteractionEnded -= DestroyCollectedObject;

            if (_collectibleController != null)
            {
                _collectibleController.DestroyCollectible();
                _collectibleController = null;
            }
        }
    }
}