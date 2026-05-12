using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private AudioClip _currentClip;

    [Header("Surface Type Sound Values")]
    [SerializeField] private SurfaceMovementSoundsScriptableObject _gravelSurfaceSounds;
    [SerializeField] private SurfaceMovementSoundsScriptableObject _grassSurfaceSounds;
    [SerializeField] private SurfaceMovementSoundsScriptableObject _leavesSurfaceSounds;
    private SurfaceMovementSoundsScriptableObject _currentSurfaceTypeSounds;
    private SurfaceType _currentSurfaceType;
    private PlayerState _currentPlayerState;

    private bool IsPlayingAudio = false;

    public void PlaySurfaceMovementAudio(SurfaceType surface, PlayerState playerState)
    {
        _currentSurfaceType = surface;
        _currentPlayerState = playerState;

        switch (surface)
        {
            case SurfaceType.Gravel:
                _currentSurfaceTypeSounds = _gravelSurfaceSounds;
                break;
            case SurfaceType.Grass:
                _currentSurfaceTypeSounds = _grassSurfaceSounds;
                break;
            case SurfaceType.Leaves:
                _currentSurfaceTypeSounds = _leavesSurfaceSounds;
                break;
            default: 
                _currentSurfaceTypeSounds = _gravelSurfaceSounds;
                break;
        }

        if (!IsPlayingAudio)
        {
            StartCoroutine(PlayAudio());
        }
    }

    private IEnumerator PlayAudio()
    {
        if (!IsPlayingAudio)
        {  
            IsPlayingAudio = true;
            AudioClip[] currentStateSurfaceSounds = null;

            switch (_currentPlayerState)
            {
                case PlayerState.Walking:
                    currentStateSurfaceSounds = _currentSurfaceTypeSounds.walkingSounds;
                    break;
                case PlayerState.Running:
                    currentStateSurfaceSounds = _currentSurfaceTypeSounds.runningSounds;
                    break;
                default: 
                    currentStateSurfaceSounds = _currentSurfaceTypeSounds.walkingSounds;
                    break;
            }

            int clipIndex = Random.Range(0, currentStateSurfaceSounds.Length);

            _currentClip = currentStateSurfaceSounds[clipIndex];

            _audioSource.clip = _currentClip;
            _audioSource.Play();

            yield return new WaitForSeconds(_currentClip.length);

            IsPlayingAudio = false;
        }
    }
}
