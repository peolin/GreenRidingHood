using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameEventSO", menuName = "Scriptable Objects/GameEventSO")]
public class GameEventSO : ScriptableObject
{
    /*private readonly List<GameEventListener> _listeners = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].OnEventRaised();
    }

    public void RegisterListener(GameEventListener listener) => _listeners.Add(listener);
    public void UnregisterListener(GameEventListener listener) => _listeners.Remove(listener);
    // can be upscaled if an event should trigger action in multiple modules simultaneously */ 
    public System.Action OnEventRaised;

    public void Raise() => OnEventRaised?.Invoke();
}
