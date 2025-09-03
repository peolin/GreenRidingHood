using UnityEngine;

[CreateAssetMenu(fileName = "SpawnManagerScriptableObject", menuName = "SpawnManagerScriptableObject", order = 1)]
public class SpawnManagerScriptableObject : ScriptableObject
{
    public string prefabName;

    public int numberOfPrefabsToCreate;
    public Vector3[] spawnPoints;
}
