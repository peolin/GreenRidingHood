using UnityEngine;

[CreateAssetMenu(fileName = "NarrativePointConfiguration", menuName = "Scriptable Objects/NarrativePointConfiguration")]
public class NarrativePointConfig : ScriptableObject
{
    public NarrativePoint Point;
    public bool FreezePlayer;
    public string[] PointLines;

    public GameObject PrefabToSpawn;
}
