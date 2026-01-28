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

public class PlayerSurfaceDetector : MonoBehaviour
{
    private SurfaceType _currentSurfaceType = SurfaceType.Gravel;

    private void OnEnable()
    {
        PlayerBehaviour.OnPlayerStateChanged += HandlePlayerState;
    }
    private void OnDisable()
    {
        PlayerBehaviour.OnPlayerStateChanged -= HandlePlayerState;
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

    private void HandlePlayerState(PlayerState newState)
    {
        if (newState != PlayerState.Idle & newState != PlayerState.Jumping)
        {
            AudioManager.Instance.PlaySurfaceMovementAudio(_currentSurfaceType, newState);
        }
    }
}
