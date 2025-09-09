using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SurfaceType
{
    Gravel,
    Grass,
    Leaves,
    DirtyGround,
    Sand,
    Water,
    Wood
}

public class MovementAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private AudioClip _currentClip;

    private SurfaceType _currentSurfaceType;
    [Header("Surface Type Sound Values")]
    [SerializeField] private SurfaceMovementSoundsScriptableObject _gravelSurfaceSounds;
    [SerializeField] private SurfaceMovementSoundsScriptableObject _grassSurfaceSounds;
    [SerializeField] private SurfaceMovementSoundsScriptableObject _leavesSurfaceSounds;
    private SurfaceMovementSoundsScriptableObject _currentSurfaceTypeSounds;

    private PlayerState _currentPlayerState;

    private bool IsPlayingAudio = false;

    private void Awake()
    {
        PlayerBehaviour.OnPlayerStateChanged += HandlePlayerState;
    }

    private void HandlePlayerState(PlayerState currentState)
    {
        _currentPlayerState = currentState;
    }

    public void OnTriggerStay(Collider other)
    {
        if (!IsPlayingAudio)
        {
            string otherTag = other.tag;
            switch (otherTag)
            {
                case "Gravel":
                    _currentSurfaceType = SurfaceType.Gravel;
                    _currentSurfaceTypeSounds = _gravelSurfaceSounds;
                    break;
                case "Grass":
                    _currentSurfaceType = SurfaceType.Grass;
                    _currentSurfaceTypeSounds = _grassSurfaceSounds;
                    break;
                case "Leaves":
                    _currentSurfaceType = SurfaceType.Leaves;
                    _currentSurfaceTypeSounds = _leavesSurfaceSounds;
                    break;
                default: break;
            }
            
            if (_currentPlayerState != PlayerState.Idle & _currentPlayerState != PlayerState.Jumping)
            {
                StartCoroutine(PlaySurfaceMovementAudio());
            }
        }
    }

    private IEnumerator PlaySurfaceMovementAudio()
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
            default: break;
        }

        int clipIndex = Random.Range(1, currentStateSurfaceSounds.Length);

        _currentClip = currentStateSurfaceSounds[clipIndex];

        _audioSource.clip = _currentClip;
        _audioSource.Play();

        yield return new WaitForSeconds(_currentClip.length);

        IsPlayingAudio = false;
    }

}
