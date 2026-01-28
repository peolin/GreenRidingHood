using Collectibles;
using UnityEngine;

public class CollectiblesAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _collectiblesAudioSource;
    [SerializeField] private AudioClip _collectibleAudioClip;

    /*private void OnEnable()
    {
        CollectiblesManager.OnObjectCollection += PlayCollectibleAudio;
    }
    private void OnDisable()
    {
        CollectiblesManager.OnObjectCollection -= PlayCollectibleAudio;
    }*/

    public void PlayCollectibleAudio()
    {
        _collectiblesAudioSource.clip = _collectibleAudioClip;
        _collectiblesAudioSource.Play();
    }
}
