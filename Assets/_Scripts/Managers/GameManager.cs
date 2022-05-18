using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.PlayerInfo;

public class GameManager : Singleton<GameManager>
{
    public float matchRound = 1;

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

    public void ModeAttacker(float value)
    {

    }

    public void ModeDefender()
    {

    }

    public void Round()
    {

    }
}
