using System;
using UnityEngine;

public class Teleporter : Interactable
{
    [SerializeField] private Scenario target;
    private Movement _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Movement>();
    }

    public override void Interact()
    {
        if(!IsInteractable) return;
        base.Interact();

        CameraMovement.Instance.MoveCamera(target);
        _player.ForceMovement(target.SpawnPoint);
    }
}
