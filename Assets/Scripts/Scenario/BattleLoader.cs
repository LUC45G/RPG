using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleLoader : Interactable
{
    
    public override void Interact()
    {
        if(!IsInteractable) return;
        
        base.Interact();
        SceneManager.LoadScene(2);
    }
}
