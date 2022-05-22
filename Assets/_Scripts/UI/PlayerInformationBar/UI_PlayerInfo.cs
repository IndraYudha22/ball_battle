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

        private PlayerCondition player;
        private PlayerCondition enemy;

        public delegate void StatusPlayer();
        public StatusPlayer statusPlayer;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            this.textInformation.text = $"{this.playerInfoName} ({InfoStatus(this.fractions)})";
            SubscribeGameManager();
        }

        private void Update()
        {
            RefillEnergy();
        }

        private string InfoStatus(Fractions fractions)
        {
            player = Parameters.GetPlayerCondition(ModePlayer.attacker);
            enemy = Parameters.GetPlayerCondition(ModePlayer.defender);

            if (fractions == Fractions.player)
            {
                return player.status;
            }
            else
            {
                return enemy.status;
            }

        }
    }
}
