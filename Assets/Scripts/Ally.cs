using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{
     public string charaname;
    public int maxHP;
    public int currentHP;
    public int str;
    public int vel;

    public int maxEnergy;
    public int currentEnergy;
    public bool isdefend = false;

    public Animator animacionpersonaje;


    public bool takedamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void heal(int value)
    {
        if (currentHP + value >= maxHP)
        {
            currentHP = maxHP;
        }
        else
        {
            currentHP += value;
        }
    }

    public bool consumeEspecial(int value)
    {
        if (currentEnergy - value <= 0)
        {
            return false;
        }
        else
        {
            currentEnergy -= value;
            return true;
        }
        
    }
}
