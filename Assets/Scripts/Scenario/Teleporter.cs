using System;
using UnityEngine;

public class Teleporter : Interactable
{
    [Header("Teleporter")]
    [Space]
    [SerializeField] private Scenario myScenario;
    [SerializeField] private Scenario target;
    [SerializeField] private bool movableSpawn;
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
        
        if (movableSpawn) myScenario.SpawnPoint = _player.transform.position;
        
        _player.ForceMovement(target.SpawnPoint);
    }
}
