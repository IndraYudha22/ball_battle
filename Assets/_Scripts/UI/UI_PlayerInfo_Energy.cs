using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerInfo
{
    public partial class UI_PlayerInfo
    {
        [Header("ENERGY")]
        [SerializeField] private List<EnergyPart> energyPart;
        [SerializeField] private SO_ColorFraction colorFraction;
        private float energyFill = 0;

        public float GetCurrentEnergy(Fractions fractions)
        {
            if (fractions == this.fractions)
            {
                return energyFill;
            }
            return -1;
        }

        public void DecreaseEnergy(Fractions fractions, float value)
        {
            if (fractions == this.fractions)
            {
                Debug.Log($"DECREASE: {value}");
                energyFill -= value;
            }
        }

        private void RefillEnergy()
        {
            if (energyFill < Parameters.maxEnergyBar)
            {
                energyFill += Parameters.energyRegeneration * Time.deltaTime;

                int _part = (int)energyFill;
                float _energyFill = energyFill - _part;

                for (int i = 0; i < _part; i++)
                {
                    EnergyPart _energyPart = energyPart[i];
                    _energyPart.SetValueEnergy(fractions, 1);
                }

                if (energyFill >= Parameters.maxEnergyBar) return;

                for (int i = _part; i < energyPart.Count; i++)
                {
                    EnergyPart _energyPart = energyPart[i];
                    _energyPart.SetValueEnergy(fractions, 0);
                }

                energyPart[_part].SetValueEnergy(fractions, _energyFill);

            }
        }
    }
}

