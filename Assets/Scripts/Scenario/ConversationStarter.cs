using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Events;

public class ConversationStarter : Interactable
{

    [Space]
    [SerializeField] private Conversation conversation;
    [SerializeField] private Collider2D myCollider;

    [Space] 
    [SerializeField] private UnityEvent onConversationEnded;
    
    private bool _started;

    private void Awake()
    {
        conversation.Initialize();
    }

    public override void Interact()
    {
        sfxmanager.sfxinstance.Audio.PlayOneShot(sfxmanager.sfxinstance.dialogo);
        if(!IsInteractable) return;
        base.Interact();
        
        HandleFirstDialog();
        Unhover();
    }

    private GameObject _currentActive;

    private void Update()
    {
        if (!_started) return;

        if (!Input.GetButtonDown("Interact")) return;

        HandleNextDialog();
    }

    private void HandleNextDialog()
    {
        if (_currentActive) _currentActive.SetActive(false);
        
        if (conversation.Finished)
        {
            _started = false;
            myCollider.enabled = false;
            Unhover();
            onConversationEnded?.Invoke();
            return;
        }
        

        var d = conversation.GetNextDialog();

        var dialog = d.Text;
        var act = d.Actor;
        var text = d.TextUI;

        text.text = dialog;
        act.SetActive(true);

        _currentActive = act;
    }

    private void HandleFirstDialog()
    {
        var d = conversation.GetNextDialog();

        var dialog = d.Text;
        var act = d.Actor;
        var text = d.TextUI;

        text.text = dialog;
        act.SetActive(true);

        _currentActive = act;
        StartCoroutine(Asco());

        IEnumerator Asco()
        {
            yield return new WaitForSeconds(.1f);
            _started = true;
        }
            
    }

}
