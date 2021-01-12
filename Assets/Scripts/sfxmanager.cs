using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxmanager : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip click;
    public AudioClip medialuna;
    public AudioClip engrapadora;
    public AudioClip tijera;

    public AudioClip golpegolem;
    public AudioClip golemrugido;

    public AudioClip dialogo;

    public AudioClip tijeraattack;
    public AudioClip grapespecial;

    public static sfxmanager sfxinstance;

    private void Awake() 
    {
        if (sfxinstance != null && sfxinstance != this)
        {
            Destroy(this.gameObject);
            return;
        }    

        sfxinstance = this;
        DontDestroyOnLoad(this);
    }    
}
