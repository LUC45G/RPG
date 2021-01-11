using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyname;
    public int maxHP;
    public int currentHP;
    public int str;
    public int vel;

    public bool takedamage(int damege)
    {
        currentHP -= damege;

        if (currentHP <= 0)
        {
            if (currentHP == 0)
            {
                return true;
            }
            else
            {
                currentHP = maxHP;
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
