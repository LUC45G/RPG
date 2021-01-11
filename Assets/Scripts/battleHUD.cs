using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battleHUD : MonoBehaviour
{
    public Slider HPSlider;

    public Slider energySlider;

    public GameObject HUDvisible;

    public Text textovidajefe;

    public void setHUD(Ally ally)
    {
        HUDvisible.SetActive(true);
        HPSlider.maxValue = ally.maxHP;
        HPSlider.value = ally.currentHP;
        energySlider.maxValue = ally.maxEnergy;
        energySlider.value = ally.currentEnergy;
    }

    public void setenemyHUD(Enemy enemy)
    {
        HPSlider.maxValue = enemy.maxHP;
        HPSlider.value = enemy.currentHP;
        textovidajefe.text = enemy.currentHP.ToString();
    }

    public void sethp(int value)
    {
        HPSlider.value = value;
    }

    public void sethpenemigo(int value)
    {
        HPSlider.value = value;
        textovidajefe.text = value.ToString();
    }
    public void setenergy(int value)
    {
        energySlider.value = value;
    }
}
