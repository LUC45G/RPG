using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private GameObject eText;

    protected bool IsInteractable = true;
    
    public virtual void Interact()
    {
        if(!IsInteractable) return;
        
        IsInteractable = false;
        Unhover();

        StartCoroutine(ReenableInteraction());

        IEnumerator ReenableInteraction()
        {
            if (cooldown < 10)
            {
                yield return new WaitForSeconds(cooldown);
                IsInteractable = true;
            }
        }
    }

    protected bool Hovered;

    public virtual void Hover()
    {
        if (Hovered) return;
        Hovered = true;
        
        eText.SetActive(true);
    }
    
    public virtual void Unhover()
    {
        if (!Hovered) return;
        Hovered = false;
        
        eText.SetActive(false);
    }
}
