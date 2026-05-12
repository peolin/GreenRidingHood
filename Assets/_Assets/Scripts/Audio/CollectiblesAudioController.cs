using Collectibles;
using UnityEngine;

public class CollectiblesAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _collectiblesAudioSource;
    [SerializeField] private AudioClip _collectibleAudioClip;
    
    [SerializeField] private GameEventSO _onCollectedEvent;

    private void OnEnable() => _onCollectedEvent.OnEventRaised += PlayCollectibleAudio;
    private void OnDisable() => _onCollectedEvent.OnEventRaised -= PlayCollectibleAudio;
    
    public void PlayCollectibleAudio()
    {
        _collectiblesAudioSource.clip = _collectibleAudioClip;
        _collectiblesAudioSource.Play();
    }
}
