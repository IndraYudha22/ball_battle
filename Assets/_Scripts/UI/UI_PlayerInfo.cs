using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.PlayerInfo
{
    public partial class UI_PlayerInfo : MonoBehaviour
    {
        [SerializeField] private Fractions fractions;
        [SerializeField] private string playerInfoName;

        [SerializeField] private TextMeshProUGUI textInformation;

        private void Awake()
        {
            textInformation.text = $"{textInformation.text}";
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                DecreaseEnergy(Fractions.player);
            }
            RefillEnergy();
        }
    }
}
