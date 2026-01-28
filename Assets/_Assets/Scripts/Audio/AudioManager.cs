using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AmbianceAudioController _ambianceAudioController;
    [SerializeField] private CollectiblesAudioController _collectiblesAudioController;
    [SerializeField] private MovementAudioController _movementAudioController;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _ambianceAudioController.PlayAmbianceAudio();
    }

    public void PlaySurfaceMovementAudio(SurfaceType surface, PlayerState playerState)
    {
        if (playerState != PlayerState.Idle & playerState != PlayerState.Jumping)
        {
            _movementAudioController.PlaySurfaceMovementAudio(surface, playerState);
        }
    }

    public void PlayCollectibleAudio()
    {
        _collectiblesAudioController.PlayCollectibleAudio();
    }
}
