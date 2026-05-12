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

        [SerializeField] private GameEventSO _pickedCollectibleEvent;
        public event Action OnObjectCollection;

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

            //AudioManager.Instance.PlayCollectibleAudio();

            _collectibleController = triggeredController;
            
            _pickedCollectibleEvent.Raise();
            OnObjectCollection?.Invoke();

            if (_uiManager != null)
            {
                _uiManager.OnUIInteractionEnded -= DestroyCollectedObject;
                _uiManager.OnUIInteractionEnded += DestroyCollectedObject;
            }
            else Debug.LogError("$CollectiblesManager: UIManager not assigned!");
        }

        private void DestroyCollectedObject() // shift to using a pool
        {
            _uiManager.OnUIInteractionEnded -= DestroyCollectedObject;

            if (_collectibleController != null)
            {
                _collectibleController.DestroyCollectible();
                _collectibleController = null;
            }
            else Debug.LogError("$CollectiblesManager: no collectible to destroy!");
        }
    }
}