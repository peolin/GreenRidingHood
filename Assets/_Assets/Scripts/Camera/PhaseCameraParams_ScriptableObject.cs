using UnityEngine;

[CreateAssetMenu(fileName = "PhaseCameraParams_ScriptableObject", menuName = "CameraParameters", order = 2)]
public class PhaseCameraParams_ScriptableObject : ScriptableObject
{
    public Vector3 cameraWorldPosition;
    public Vector3 cameraRotation; 
}
