using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Battlestates{START, PLAYERTURN, PLAYERTURN2, ENEMYTURN, WON, LOST}

public class Gamemanager : MonoBehaviour
{
    public Battlestates estado;
    public GameObject[] playersPrefabs;
    public GameObject[] enemyPrefabs;
    public Transform[] playerbattlestation;
    public Transform[] enemybattlestation;
    Enemy enemyunit;
    public Ally playerunit;
    public battleHUD[] playerHUD2;
    public battleHUD enemyHUD;
    public GameObject textannuncer;
    public GameObject[] portraits;
    public GameObject[] ationbuttons;
    public Text Actiontext;

    // Start is called before the first frame update
    void Start()
    {
        estado = Battlestates.START;
        StartCoroutine (Setupbattle());
    }

    IEnumerator Setupbattle()
    {
        for ( int i = 0; i < playersPrefabs.Length; i++)
        {
            if (playersPrefabs[i] != null)
            {
                GameObject playerGO = Instantiate(playersPrefabs[i], playerbattlestation[i]);
                playerunit = playerGO.GetComponent<Ally>();
                playerHUD2[i].setHUD(playerunit);
            } 
        }
        
        GameObject enemyGO = Instantiate(enemyPrefabs[0], enemybattlestation[0]);
        enemyunit = enemyGO.GetComponent<Enemy>();

        textannuncer.SetActive(true);
        Actiontext.text = "El " + enemyunit.enemyname + " se acerca...";

        //playerHUD.setHUD(playerunit);
        enemyHUD.setenemyHUD(enemyunit);

        yield return new WaitForSeconds(2f);
        estado = Battlestates.PLAYERTURN;
        StartCoroutine (Playerturn());
    }

    public void disablebuttons()
    {
        for (int i = 0; i < ationbuttons.Length; i++)
        {
            ationbuttons[i].GetComponent<Button>().interactable = false;
        }
    }
    IEnumerator Playerturn()
    {
        for (int i = 0; i < playersPrefabs.Length; i++)
        {
            if (playersPrefabs[i] != null)
            {
                portraits[i].SetActive(false);
            }
        }
        for (int i = 0; i < ationbuttons.Length; i++)
        {
            ationbuttons[i].GetComponent<Button>().interactable = true;
        }
        if (estado == Battlestates.PLAYERTURN)
        {
            GameObject playerGO = playersPrefabs[0];
            playerunit = playerGO.GetComponent<Ally>();
            portraits[0].SetActive(true);
        }
        else if (estado == Battlestates.PLAYERTURN2)
        {
            GameObject playerGO = playersPrefabs[1];
            playerunit = playerGO.GetComponent<Ally>();
            portraits[1].SetActive(true);
        }

        playerunit.isdefend = false;
        textannuncer.SetActive(true);
        Actiontext.text = "¿Ya puedo irme a casa?";

        yield return new WaitForSeconds(1f);

        textannuncer.SetActive(false);
    }
    
    IEnumerator Enemyturn()
    {
        bool isdead;

        int objetivo = Random.Range(0,2);
        Debug.Log(objetivo);
        GameObject playerGO = playersPrefabs[objetivo];
        playerunit = playerGO.GetComponent<Ally>();

        sfxmanager.sfxinstance.Audio.PlayOneShot(sfxmanager.sfxinstance.golpegolem);
        yield return new WaitForSeconds(3f);
        textannuncer.SetActive(true);

        if (playerunit.isdefend == true)
        {
            Actiontext.text = playerunit.charaname + " recibe " + enemyunit.str/2 + " de daño!";
            isdead = playerunit.takedamage(enemyunit.str/2);
        }
        else
        {
            Actiontext.text = playerunit.charaname + " recibe " + enemyunit.str + " de daño!";
            isdead = playerunit.takedamage(enemyunit.str);
        }
        
        playerHUD2[objetivo].sethp(playerunit.currentHP);

        yield return new WaitForSeconds(1.5f);

        if (isdead)
        {
            estado = Battlestates.LOST;
            Endbattle();
        }
        else
        {
            estado = Battlestates.PLAYERTURN;
            StartCoroutine (Playerturn());
        }
    }

    public void OnAttackbutton()
    {
        disablebuttons();
        sfxmanager.sfxinstance.Audio.PlayOneShot(sfxmanager.sfxinstance.click);
        if (estado != Battlestates.PLAYERTURN && estado != Battlestates.PLAYERTURN2)
        {
            return;
        }
        StartCoroutine (playerattack());
    }

    public void OnSpecialbutton()
    {
        disablebuttons();
        sfxmanager.sfxinstance.Audio.PlayOneShot(sfxmanager.sfxinstance.click);
        if (estado != Battlestates.PLAYERTURN && estado != Battlestates.PLAYERTURN2)
        {
            return;
        }
        StartCoroutine (playerespecial());
    }

    public void OnDefendbutton()
    {
        disablebuttons();
        sfxmanager.sfxinstance.Audio.PlayOneShot(sfxmanager.sfxinstance.click);
        if (estado != Battlestates.PLAYERTURN && estado != Battlestates.PLAYERTURN2)
        {
            return;
        }
        StartCoroutine (Playerdefend());
    }

    public void OnItembutton()
    {
        disablebuttons();
        sfxmanager.sfxinstance.Audio.PlayOneShot(sfxmanager.sfxinstance.click);

        sfxmanager.sfxinstance.Audio.PlayOneShot(sfxmanager.sfxinstance.medialuna);
        if (estado != Battlestates.PLAYERTURN && estado != Battlestates.PLAYERTURN2)
        {
            return;
        }
        StartCoroutine (PlayerItems());
    }

    IEnumerator PlayerItems()
    {
        if (estado == Battlestates.PLAYERTURN)
        {
            playerunit.heal(20);
            playerHUD2[0].sethp(playerunit.currentHP);
            textannuncer.SetActive(true);

            Actiontext.text = "Tremenda medialuna, te curas 20 de vida!";

            yield return new WaitForSeconds(2f);
        
            estado = Battlestates.PLAYERTURN2;
            StartCoroutine(Playerturn());
        }
        else if (estado == Battlestates.PLAYERTURN2)
        {
            playerunit.heal(20);
            playerHUD2[1].sethp(playerunit.currentHP);

            textannuncer.SetActive(true);

            Actiontext.text = "Tremenda medialuna, te curas 20 de vida!";

            yield return new WaitForSeconds(2f);
        
            estado = Battlestates.ENEMYTURN;
            StartCoroutine(Enemyturn());
        }
    }

    IEnumerator playerespecial()
    {
        bool CanUseEspecial = playerunit.consumeEspecial(10);

        if (CanUseEspecial)
        {
            if (estado == Battlestates.PLAYERTURN)
            {
                playerHUD2[0].setenergy(playerunit.currentEnergy);
            }
            else if (estado == Battlestates.PLAYERTURN2)
            {
                playerHUD2[1].setenergy(playerunit.currentEnergy);
            }
            

            bool isdead = enemyunit.takedamage(playerunit.str*2);
            enemyHUD.sethp(enemyunit.currentHP);

            textannuncer.SetActive(true);
            Actiontext.text = "Haces " + playerunit.str*2 + " de daño!";

            yield return new WaitForSeconds(2f);


            estado = Battlestates.ENEMYTURN;
            StartCoroutine(Enemyturn());
        }
        else
        {
            textannuncer.SetActive(true);
            Actiontext.text = "No creo poder hacer eso...";

            yield return new WaitForSeconds (2f);

            StartCoroutine(Playerturn());
        }
        
    }

    IEnumerator Playerdefend()
    {
        playerunit.isdefend = true;

        textannuncer.SetActive(true);
        Actiontext.text = "Esto no dolera mucho. ¿verdad?";

        yield return new WaitForSeconds(2f);

         if (estado == Battlestates.PLAYERTURN)
        {
            estado = Battlestates.PLAYERTURN2;
            StartCoroutine(Playerturn());
        }
        else if (estado == Battlestates.PLAYERTURN2)
        {
            estado = Battlestates.ENEMYTURN;
            StartCoroutine(Enemyturn());
        }
        
    }

    IEnumerator playerattack()
    {
        bool isdead = enemyunit.takedamage(playerunit.str);
        enemyHUD.sethp(enemyunit.currentHP);
        textannuncer.SetActive(true);
        Actiontext.text = "Haces " + playerunit.str + " de daño!";

        yield return new WaitForSeconds(1f);

        textannuncer.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        if(isdead)
        {
            //terminar combate
            estado = Battlestates.WON;
            Endbattle();
        }
        else
        {
            //pasar turno
            if (estado == Battlestates.PLAYERTURN)
            {
                estado = Battlestates.PLAYERTURN2;
                StartCoroutine(Playerturn());
            }
            else if (estado == Battlestates.PLAYERTURN2)
            {
                estado = Battlestates.ENEMYTURN;
                StartCoroutine(Enemyturn());
            }
            
        }
        
    }

    void Endbattle()
    {
        if (estado == Battlestates.WON)
        {
            textannuncer.SetActive(true);
            Actiontext.text = "¿Ganamos? bien necesito un café";
        }
        else if (estado == Battlestates.LOST)
        {
            textannuncer.SetActive(true);
            Actiontext.text = "Oh... bueno perdiste.";
        }
    }

}
