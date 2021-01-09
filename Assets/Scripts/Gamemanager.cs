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

        yield return new WaitForSeconds(2f);
        estado = Battlestates.PLAYERTURN;
        Playerturn();
    }

    void Playerturn()
    {
        textannuncer.SetActive(true);
        Actiontext.text = "¿Ya puedo irme a casa?";

        
    }

}
