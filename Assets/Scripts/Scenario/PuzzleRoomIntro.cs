using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoomIntro : MonoBehaviour
{
    [SerializeField] private Collider2D myCollider;
    [SerializeField] private ConversationStarter with, without;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        if(without.isActiveAndEnabled)
            without.Interact();
        else
            with.Interact();

        myCollider.enabled = false;

    }
}
