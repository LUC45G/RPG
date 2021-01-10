using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Battlestates{START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class Gamemanager : MonoBehaviour
{
    public Battlestates estado;
    public GameObject[] playersPrefabs;
    public GameObject[] enemyPrefabs;
    public Transform[] playerbattlestation;
    public Transform[] enemybattlestation;
    Enemy enemyunit;
    Ally playerunit;

    public battleHUD playerHUD;
    public battleHUD enemyHUD;
    public GameObject textannuncer;
    public Text Actiontext;

    // Start is called before the first frame update
    void Start()
    {
        estado = Battlestates.START;
        StartCoroutine (Setupbattle());
    }

    IEnumerator Setupbattle()
    {
        GameObject playerGO = Instantiate(playersPrefabs[0], playerbattlestation[0]);
        playerunit = playerGO.GetComponent<Ally>();

        GameObject enemyGO = Instantiate(enemyPrefabs[0], enemybattlestation[0]);
        enemyunit = enemyGO.GetComponent<Enemy>();

        textannuncer.SetActive(true);
        Actiontext.text = "El " + enemyunit.enemyname + " se acerca...";

        playerHUD.setHUD(playerunit);
        enemyHUD.setenemyHUD(enemyunit);

        yield return new WaitForSeconds(2f);
        estado = Battlestates.PLAYERTURN;
        StartCoroutine (Playerturn());
    }

    IEnumerator Playerturn()
    {
        playerunit.isdefend = false;
        textannuncer.SetActive(true);
        Actiontext.text = "¿Ya puedo irme a casa?";

        yield return new WaitForSeconds(1f);

        textannuncer.SetActive(false);
    }
    
    IEnumerator Enemyturn()
    {
        bool isdead;
        textannuncer.SetActive(true);

        yield return new WaitForSeconds(1f);

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
        
        playerHUD.sethp(playerunit.currentHP);

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
        if (estado != Battlestates.PLAYERTURN)
        {
            return;
        }
        StartCoroutine (playerattack());
    }

    public void OnSpecialbutton()
    {
        if (estado != Battlestates.PLAYERTURN)
        {
            return;
        }
        StartCoroutine (playerespecial());
    }

    public void OnDefendbutton()
    {
        if (estado != Battlestates.PLAYERTURN)
        {
            return;
        }
        StartCoroutine (Playerdefend());
    }

    public void OnItembutton()
    {
        if (estado != Battlestates.PLAYERTURN)
        {
            return;
        }
        StartCoroutine (PlayerItems());
    }

    IEnumerator PlayerItems()
    {
        playerunit.heal(20);
        playerHUD.sethp(playerunit.currentHP);

        textannuncer.SetActive(true);
        Actiontext.text = "Tremenda medialina, te curas 20 de vida!";

        yield return new WaitForSeconds(2f);

        estado = Battlestates.ENEMYTURN;
        StartCoroutine(Enemyturn());
    }

    IEnumerator playerespecial()
    {
        bool CanUseEspecial = playerunit.consumeEspecial(10);

        if (CanUseEspecial)
        {
            playerHUD.setenergy(playerunit.currentEnergy);

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

        estado = Battlestates.ENEMYTURN;
        StartCoroutine(Enemyturn());
    }

    IEnumerator playerattack()
    {
        bool isdead = enemyunit.takedamage(playerunit.str);
        enemyHUD.sethp(enemyunit.currentHP);
        
        Actiontext.text = "Haces " + playerunit.str + " de daño!";

        yield return new WaitForSeconds(1f);

        textannuncer.SetActive(false);
        if(isdead)
        {
            //terminar combate
            estado = Battlestates.WON;
            Endbattle();
        }
        else
        {
            //pasar turno
            estado = Battlestates.ENEMYTURN;
            StartCoroutine(Enemyturn());
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
