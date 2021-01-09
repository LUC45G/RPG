using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    #region Singleton

    private static CameraMovement _instance;

    public static CameraMovement Instance
    {
        get
        {
            if(_instance == null)
                _instance = FindObjectOfType<CameraMovement>();
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
    #endregion

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float xSnap, ySnap;

    public void MoveCamera(int xDir, int yDir)
    {
        var p = cameraTransform.position;
        p += xDir * xSnap * Vector3.right;
        p += yDir * ySnap * Vector3.up;
        cameraTransform.position = p;
    }
}
