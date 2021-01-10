using System;
using UnityEngine;

public class Teleporter : Interactable
{
    [SerializeField] private Scenario target;
    [SerializeField] private bool savePosition;
    [SerializeField] private Transform mySpawn;
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
        if (savePosition) mySpawn.position = _player.transform.position;
        _player.ForceMovement(target.SpawnPoint);
    }
}
