using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Conversation : ScriptableObject
{
    public List<string> conversation;

    private int _index;
    private List<Dialog> _dialogs;

    public void Initialize() 
    {
        if (conversation == null) return;
        _index = 0;
        _dialogs = new List<Dialog>();
        
        foreach(var d in conversation)
        {
            var aux = d.Split(':');
            _dialogs.Add(new Dialog(aux[0], aux[1]));
        }
    }

    public Dialog GetNextDialog()
    {
        if (_index >= _dialogs.Count) return null;
        return _dialogs[_index++];
    }

    public bool Finished => _index >= _dialogs.Count;
} 

public class Dialog
{
    public string Text { get; private set; }
    public GameObject Actor { get; private set; }
    public Text TextUI { get; private set; }
    
    public Dialog(string a, string t)
    {
        Text = t;
        Actor = GameObject.Find(a);
        TextUI = Actor.GetComponentInChildren<Text>();
    }
}
