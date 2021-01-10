using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    [SerializeField] private Transform spawn, cameraTransform;

    public Vector3 SpawnPoint => spawn.position;
    public Vector3 CameraPosition => cameraTransform.position;
    
}
