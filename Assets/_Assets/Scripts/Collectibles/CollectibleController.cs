using System;
using UnityEngine;

namespace Collectibles
{
    public class CollectibleController : MonoBehaviour
    {
        public static event Action<CollectibleController> OnCollectibleTrigger;

        private void OnTriggerEnter()
        {
            OnCollectibleTrigger?.Invoke(this);
        }

        public void DestroyCollectible()
        {
            Destroy(gameObject);
        }
    }
}