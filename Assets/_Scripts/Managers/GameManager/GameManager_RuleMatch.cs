using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Timer;

public partial class GameManager
{
    [Header("RULE MATCH")]
    public int matchRound = 1;
    private int playerPoint;
    private int enemyPoint;
    private string statusWin;

    public delegate void OnReset();
    public event OnReset onReset;

    public delegate void ResultGame(int playerScore, int enemyScore, int round, string statusWin);
    public ResultGame resultGame;

    public void Draw()
    {
        UI_Timer.Instance.onTimer = () =>
        {
            onReset();
        };
    }

    public void GameOver()
    {
        Parameters.startTimer = false;
        Parameters.fillEnergy = false;
    }

    public void ResultMatch(Fractions fractions)
    {
        if (fractions == Fractions.player)
        {
            playerPoint++;
        }
        else if (fractions == Fractions.enemy)
        {
            enemyPoint++;
        }
        
        onReset();
        CalculateMatchTheGame();
        resultGame?.Invoke(playerPoint, enemyPoint, matchRound, statusWin);
        matchRound++;
    }

    public void CalculateMatchTheGame()
    {
        statusWin = "";

        foreach (AttackerSoldier soldier in listAttackerSoldiers)
        {
            Destroy(soldier.gameObject);
        }

        foreach (DefenderSoldier soldier in listDefenderSoldiers)
        {
            Destroy(soldier.gameObject);
        }

        if (matchRound < Parameters.roundGame)
        {
            if (playerPoint >= 3 && enemyPoint <= 1)
            {
                statusWin = "PLAYER WIN";
            }
            else if (enemyPoint >= 3 && playerPoint <= 1)
            {
                statusWin = "ENEMY WIN";
            }
        }
        else
        {
            if (playerPoint > enemyPoint)
            {
                // player win
                statusWin = "PLAYER WIN";
            }
            else if (playerPoint < enemyPoint)
            {
                // enemy win
                statusWin = "ENEMY WIN";
            }
            else
            {
                statusWin = "DRAW";
                // penalty for player
            }
        }

        UIManager.Instance.ShowCanvas(SelectCanvas.round);
    }
}
