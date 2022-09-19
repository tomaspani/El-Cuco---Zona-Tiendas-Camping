using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public int CurrentEnergy;
    public int MaxEnergy;
    public int MinEnergy;

    private void Update()
    {
        CheckEnergy();

    }

    private void CheckEnergy ()
    {
        if (CurrentEnergy > MaxEnergy)
        {
            CurrentEnergy = MaxEnergy;
        }

        if (CurrentEnergy < MinEnergy)
        {
            CurrentEnergy = MinEnergy;
        }
    }

    public void ChangeEnergy (int EnergyChange)
    {
        CurrentEnergy += EnergyChange;
    }
}
