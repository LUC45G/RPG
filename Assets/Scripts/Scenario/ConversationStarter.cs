using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationStarter : Interactable
{

    [Space]
    [SerializeField] private Conversation conversation;
    [SerializeField] private Collider2D myCollider;
    
    private bool _started;

    private void Awake()
    {
        conversation.Initialize();
    }

    public override void Interact()
    {
        if(!IsInteractable) return;
        base.Interact();

        _started = true;
        HandleNextDialog();
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

}
