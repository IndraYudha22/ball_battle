using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Timer;

public partial class GameManager
{
    [Header("RULE MATCH")]
    public float matchRound = 1;
    private int playerPoint;
    private int enemyPoint;

    public void Win()
    {
        // player win
    }

    public void Draw()
    {
        UI_Timer.Instance.onTimer = () =>
        {
            Debug.Log("GAME DRAW");
        };
    }

    public void Lose()
    {
        // player lose
    }

    public void ResultMatch(Fractions fractions)
    {
        if (fractions == Fractions.player)
        {
            playerPoint++;
            Win();
        }
        else if (fractions == Fractions.enemy)
        {
            enemyPoint++;
            Lose();
        }
    }

    public void CalculateWinnigGame()
    {
        if (matchRound < Parameters.roundGame) return;

        if (playerPoint > enemyPoint)
        {
            // player win
        }
        else if (playerPoint < enemyPoint)
        {
            // enemy win
        }
        else
        {
            // penalty for player
        }
    }
}
