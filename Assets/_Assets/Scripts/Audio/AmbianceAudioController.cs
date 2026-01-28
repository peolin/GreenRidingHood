using UnityEngine;

public class AmbianceAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _ambianceAudioSource;
    private AudioClip _currentClip;

    //[SerializeField] private AmbianceSoundsScriptableObject _ambianceSounds;
    [SerializeField] private AudioClip _ambianceAudioClip;

    /*private void Start()
    {
        PlayAmbianceAudio();
    }*/

    public void PlayAmbianceAudio()
    {
        //_currentClip = _ambianceSounds[0];
        _currentClip = _ambianceAudioClip;
        _ambianceAudioSource.clip = _currentClip;
        _ambianceAudioSource.Play();
    }
}
