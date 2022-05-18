using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerInfo
{
    public partial class UI_PlayerInfo
    {
        [Header("Properties Energy")]
        [SerializeField] private List<EnergyPart> energyPart;
        [SerializeField] private SO_ColorFraction colorFraction;
        private float energyFill = 0;

        private void SetFraction()
        {
            if (fractions == Fractions.player)
            {

            }
            else
            {

            }
        }

        private void DecreaseEnergy(Fractions fractions)
        {
            if (fractions == this.fractions){
                energyFill--;
            }
        }

        private void RefillEnergy()
        {
            if (energyFill < GameManager.Instance.maxEnergyBar)
            {
                energyFill += GameManager.Instance.energyRegeneration * Time.deltaTime;

                int _part = (int)energyFill;
                float _energyFill = energyFill - _part;

                for (int i = 0; i < _part; i++)
                {
                    EnergyPart _energyPart = energyPart[i];
                    _energyPart.SetValueEnergy(fractions, 1);
                }

                if (energyFill >= GameManager.Instance.maxEnergyBar) return;

                for (int i = _part; i < energyPart.Count; i++)
                {
                    EnergyPart _energyPart = energyPart[i];
                    _energyPart.SetValueEnergy(fractions, 0);
                }

                Debug.Log($"ENERGY FILL : {_energyFill}");
                energyPart[_part].SetValueEnergy(fractions, _energyFill);

            }
        }
    }
}

