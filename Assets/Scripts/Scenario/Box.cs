using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Interactable
{
    [SerializeField] private float xSnap, ySnap;
    
    private Transform _playerTransform;
    
    private void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }
    
    public override void Interact()
    {
        if(!IsInteractable) return;
        base.Interact();

        var myPos = transform.position;
        var player = _playerTransform.position;
        
        var xDiff = Mathf.Abs(myPos.x - player.x);
        var yDiff = Mathf.Abs(myPos.y - player.y);
        
        var xDir = xDiff > .45f ? Mathf.Sign(myPos.x - player.x) : 0f;
        var yDir = yDiff > .45f ? Mathf.Sign(myPos.y - player.y) : 0f;

        
        var direction = Vector3.right * (xDir * xSnap) + Vector3.up * (yDir * ySnap);
        //Debug.Log($"{direction} | {yDiff} - {yDir}");
        var desiredPosition = myPos + direction;
        var snap = xDir > 0 ? xSnap : ySnap;
        
        var c = Physics2D.Raycast(myPos, direction, snap).collider;
        
        if(c) {
            Debug.Log(c.name);
            return;
        }
        
        transform.position = desiredPosition;

    }
}
