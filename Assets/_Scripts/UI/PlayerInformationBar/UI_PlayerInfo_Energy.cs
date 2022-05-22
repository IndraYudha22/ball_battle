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
                energyFill -= value;
            }
        }

        private void RefillEnergy()
        {
            if (!Parameters.fillEnergy) return;

            if (energyFill < Parameters.maxEnergyBar)
            {
                energyFill += Parameters.energyRegeneration * Time.deltaTime;
                SetEnergy(energyFill);
            }
        }

        private void SetEnergy(float value)
        {
            int _part = (int)value;
            float _energyFill = value - _part;

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

        private void SubscribeGameManager()
        {
            GameManager.Instance.onReset += () =>
            {
                Parameters.fillEnergy = false;
                energyFill = 0;
                SetEnergy(0);
            };

            GameManager.Instance.setPlayerCondition += () =>
            {
                this.textInformation.text = $"{this.playerInfoName} ({InfoStatus(this.fractions)})";
            };
        }
    }
}

