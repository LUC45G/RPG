using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    [SerializeField] private Transform spawn, cameraTransform;

    public Vector3 SpawnPoint
    {
        get => spawn.position;
        set => spawn.position = value;
    }
    public Vector3 CameraPosition => cameraTransform.position;
    
}
