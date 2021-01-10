using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameObjectDisabler : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    
    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        
        foreach(var o in objects) o.SetActive(false);
        
    }
}
