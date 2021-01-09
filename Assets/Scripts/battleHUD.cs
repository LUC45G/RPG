using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battleHUD : MonoBehaviour
{
    public Text nameText;

    public Slider HPSlider;

    public Slider energySlider;

    public void setHUD(Ally ally)
    {
        nameText.text = ally.charaname;
        HPSlider.maxValue = ally.maxHP;
        HPSlider.value = ally.currentHP;
        energySlider.maxValue = ally.maxEnergy;
        energySlider.value = ally.currentEnergy;
    }

    public void sethp(int value)
    {
        HPSlider.value = value;
    }
}
