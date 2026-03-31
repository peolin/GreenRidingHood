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

[RequireComponent(typeof(PlayerBehaviour))]
public class PlayerSurfaceDetector : MonoBehaviour
{
    private SurfaceType _currentSurfaceType = SurfaceType.Gravel;
    private PlayerBehaviour _player;

    private void OnEnable()
    {
        _player.OnPlayerStateChanged += HandlePlayerStateSounds;
    }
    private void OnDisable()
    {
        _player.OnPlayerStateChanged -= HandlePlayerStateSounds;
    }

    private void Awake()
    {
        _player = GetComponent<PlayerBehaviour>();
    }

    public void OnTriggerStay(Collider other)
    {
        string otherTag = other.tag;
        switch (otherTag)
        {
            case "Gravel":
                _currentSurfaceType = SurfaceType.Gravel;
                break;
            case "Grass":
                _currentSurfaceType = SurfaceType.Grass;
                break;
            case "Leaves":
                _currentSurfaceType = SurfaceType.Leaves;
                break;
            default: 
                break;
        }
    }

    private void HandlePlayerStateSounds(PlayerState newState)
    {
        if (newState != PlayerState.Idle & newState != PlayerState.Jumping)
        {
            AudioManager.Instance.PlaySurfaceMovementAudio(_currentSurfaceType, newState); // Invoke event
        }
    }
}
