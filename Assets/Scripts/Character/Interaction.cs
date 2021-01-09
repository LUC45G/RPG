using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Movement movement;

    private Interactable _lastInter;
    
    private void Update()
    {
        if(!movement.LookingTarget) return;
        var inter = movement.LookingTarget.GetComponent<Interactable>();

        if (!inter)
        {
            if (!_lastInter) return;
            
            _lastInter.Unhover();
            _lastInter = null;
            return;
        }

        inter.Hover();
        _lastInter = inter;
        
        if (!Input.GetButtonDown("Interact")) return;
        
            
        inter.Interact();
    }
}
