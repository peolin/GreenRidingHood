using System;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    public event Action OnCollectibleTrigger;

    private void OnTriggerEnter()
    {
        OnCollectibleTrigger?.Invoke();
    }

    public void DestroyCollectible()
    {
        Destroy(gameObject);
    }
}
