using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Longpants : Interactable
{
    [Space]
    [SerializeField] private UnityEvent events;

    public override void Interact()
    {
        base.Interact();
        gameObject.SetActive(false);
        events?.Invoke();
    }
}
