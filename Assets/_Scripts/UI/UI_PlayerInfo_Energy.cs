using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerInfo
{
    public partial class UI_PlayerInfo
    {
        [Header("Properties Energy")]
        [SerializeField] private List<GameObject> energyPart;
        [SerializeField] private SO_ColorFraction colorFraction;
        private int energyPoint = 0;
        private float energyFill = 0;

        private void InitialEnergy()
        {
            // foreach (Slider part in energyPart)
            // {
            //     part.minValue = 0;
            //     part.maxValue = GameManager.Instance.maxPartEnergy;
            // }
        }

        private void ColorRefillEnergy(int pos)
        {
            var part = energyPart[pos].GetComponent<Image>();

            if (part.fillAmount < GameManager.Instance.maxPartEnergy)
            {
                part.color = colorFraction.SetColor(fractions, .5f);
            }
            else if (part.fillAmount >= GameManager.Instance.maxPartEnergy)
            {
                part.color = colorFraction.SetColor(fractions, 1f);
            }
        }

        private void RefillEnergy()
        {
            if (energyPoint <= GameManager.Instance.maxPartEnergy)
            {
                var part = energyPart[energyPoint].GetComponent<Image>();


                if (part.fillAmount < GameManager.Instance.maxPartEnergy)
                {
                    energyFill += GameManager.Instance.energyRegeneration * Time.deltaTime;
                    ColorRefillEnergy(energyPoint);
                }
                else if (part.fillAmount >= GameManager.Instance.maxPartEnergy)
                {
                    energyFill = GameManager.Instance.maxPartEnergy;
                    ColorRefillEnergy(energyPoint);
                    energyPoint++;
                }
            }
        }
    }
}

