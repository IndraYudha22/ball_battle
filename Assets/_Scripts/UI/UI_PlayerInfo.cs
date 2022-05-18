using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.PlayerInfo
{
    public partial class UI_PlayerInfo : StaticInstance<UI_PlayerInfo>
    {
        [Header("PLAYER INFO")]
        [SerializeField] private Fractions fractions;
        [SerializeField] private string playerInfoName;

        [SerializeField] private TextMeshProUGUI textInformation;

        protected override void Awake()
        {
            base.Awake();
            textInformation.text = $"{textInformation.text}";
        }

        private void Update()
        {
            RefillEnergy();
        }
    }
}
