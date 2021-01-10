using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MessagePrompter : Interactable
{
    [Header("Msg Prompter")] 
    [Space] 
    
    [SerializeField] private UnityEvent events;
    [SerializeField] private Text place;
    [SerializeField] private string text;

    public override void Interact()
    {
        if (!IsInteractable) return;
        base.Interact();

        place.text = text;
        
        events?.Invoke();
    }

    public void DisableTextInSeconds(float s) => StartCoroutine(DisableText(s));

    private IEnumerator DisableText(float s)
    {
        yield return new WaitForSeconds(s);
        place.transform.parent.gameObject.SetActive(false);
    }
}