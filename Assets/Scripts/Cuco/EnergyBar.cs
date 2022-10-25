using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;
    
    public void MaxEnergy (float MaxEnergy, float CurrentEnergy)
    {
        slider.maxValue = MaxEnergy;
        slider.value = CurrentEnergy;
    }
    public void SetEnergy(float Energy)
    {
        slider.value = Energy; 
    }
}
