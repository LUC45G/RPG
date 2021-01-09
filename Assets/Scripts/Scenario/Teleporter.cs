using System;
using UnityEngine;

public class Teleporter : Interactable
{
    [Range(-1, 1)] [SerializeField] private int x, y;
    private Movement _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Movement>();
    }

    public override void Interact()
    {
        if(!IsInteractable) return;
        base.Interact();

        CameraMovement.Instance.MoveCamera(x, y);
        _player.ForceMovement(x, y);
    }
}
