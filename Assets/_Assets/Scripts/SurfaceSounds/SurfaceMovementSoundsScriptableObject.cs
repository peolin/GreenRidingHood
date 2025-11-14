using UnityEngine;

[CreateAssetMenu(fileName = "SurfaceMovementSoundsScriptableObject", menuName = "SurfaceTypeMovementSounds", order = 1)]
public class SurfaceMovementSoundsScriptableObject : ScriptableObject
{
    public AudioClip[] walkingSounds;
    public AudioClip[] runningSounds;
    public AudioClip[] jumpingSounds;
}
