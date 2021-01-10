using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoomTeleporter : Teleporter
{
    [SerializeField] private List<Transform> objects;
    
    private Vector3[] _initials;

    public bool HasToReset { get; set; }

    private void Awake()
    {
        _initials = new Vector3[objects.Count];
        var i = 0;
        foreach (var o in objects)
        {
            _initials[i++] = o.position;
        }

        HasToReset = true;
    }

    public override void Interact()
    {
        if (!IsInteractable) return;
        base.Interact();

        if (!HasToReset) return;

        for (var i = 0; i < objects.Count; i++)
            objects[i].position = _initials[i];
        
    }

}
