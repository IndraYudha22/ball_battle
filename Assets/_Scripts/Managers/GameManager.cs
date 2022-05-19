using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.PlayerInfo;
using UI.Timer;

public partial class GameManager : StaticInstance<GameManager>
{
    [Header("GAME MANAGER")]
    public UI_PlayerInfo playerInfo;
    public UI_PlayerInfo enemyInfo;

    private PlayerCondition player;
    private PlayerCondition enemy;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        player = Parameters.GetPlayerCondition(ModePlayer.attacker);
        enemy = Parameters.GetPlayerCondition(ModePlayer.defender);

        PlayerClick();
        EnemyClick();

        Draw(); // set draw condition



    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            RandomBall(); // set random ball
        }
    }

    public void PlayerClick()
    {
        var clickHandler = PlayerController.Instance;

        clickHandler.delegateOnClickPlayer = () =>
        {
            if (playerInfo.GetCurrentEnergy(Fractions.player) > player.energyCost)
            {
                playerInfo.DecreaseEnergy(Fractions.player, player.energyCost);

                Debug.Log("ON CLICK PLAYER");
            }

        };
    }

    public void EnemyClick()
    {
        var clickHandler = PlayerController.Instance;

        clickHandler.delegateOnClickEnemy = () =>
        {
            if (enemyInfo.GetCurrentEnergy(Fractions.enemy) > enemy.energyCost)
            {
                enemyInfo.DecreaseEnergy(Fractions.enemy, enemy.energyCost);

                Debug.Log("ON CLICK ENEMY");
            }

        };
    }


}
