using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;
    
    public void MaxEnergy (int MaxEnergy, int CurrentEnergy)
    {
        slider.maxValue = MaxEnergy;
        slider.value = CurrentEnergy;
    }
    public void SetEnergy(int Energy)
    {
        slider.value = Energy; 
    }
}
