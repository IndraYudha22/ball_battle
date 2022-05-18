using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyPart : MonoBehaviour
{
    [SerializeField] private Image energyPart;
    [SerializeField] private SO_ColorFraction colorFraction;

    public void SetValueEnergy(Fractions fractions, float value)
    {
        energyPart.fillAmount = value;

        if (value > 0 && value < 1)
        {
            energyPart.color = colorFraction.SetColor(fractions, .5f);
        }
        else if (value == 1)
        {
            energyPart.color = colorFraction.SetColor(fractions, 1f);
        }
    }
}
