using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardSpriteController : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate() 
    {
        transform.rotation = mainCamera.transform.rotation;    
    }
}
