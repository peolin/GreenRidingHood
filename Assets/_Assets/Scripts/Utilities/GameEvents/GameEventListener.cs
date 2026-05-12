using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEventSO Event; // can be scaled to hold multiple
    public UnityEvent Response;

    //private void OnEnable() => Event.RegisterListener(this);
    //private void OnDisable() => Event.UnregisterListener(this);
    
    public void OnEventRaised() => Response.Invoke();
}
