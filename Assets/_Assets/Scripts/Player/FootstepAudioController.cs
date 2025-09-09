using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SurfaceType
{
    Gravel,
    Grass,
    Dirt,
    Sand,
    Water
}

public class FootstepAudioController : MonoBehaviour
{
    private SurfaceType _currentSurfaceType;

    [SerializeField] private SurfaceMovementSoundsScriptableObject _currentSurfaceTypeValues;
    [SerializeField] private AudioClip _currentClip;
    [SerializeField] private AudioSource _audioSource;

    private bool IsPlayingAudio = false;

    public void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("Finish")) & (!IsPlayingAudio))
        {
            _currentSurfaceType = SurfaceType.Gravel;
            StartCoroutine(PlaySurfaceMovementAudio());
        }
    }

    private IEnumerator PlaySurfaceMovementAudio()
    {
        IsPlayingAudio = true;
        int clipIndex = Random.Range(1, _currentSurfaceTypeValues.walkingSounds.Length);

        _currentClip = _currentSurfaceTypeValues.walkingSounds[clipIndex];

        _audioSource.clip = _currentClip;
        _audioSource.Play();

        yield return new WaitForSeconds(_currentClip.length);

        IsPlayingAudio = false;
    }

}
