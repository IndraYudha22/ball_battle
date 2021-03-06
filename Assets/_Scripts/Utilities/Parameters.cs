using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Parameters
{
    public static float scalingObject = 1f;

    public static int roundGame = 5;
    public static float timeLimit = 140f;
    public static float maxEnergyBar = 6f;

    public static float energyRegeneration = .5f;

    public static float energyCostAttacker = 2;
    public static float energyCostDefender = 3;

    public static float spawnTimeAttacker = .5f;
    public static float spawnTimeDefender = .5f;

    public static float reactivateTimeAttacker = 2.5f;
    public static float reactivateTimeDefender = 4f;

    public static float normalSpeedAttacker = 1.5f;

    public static float normalSpeedDefender = 1f;

    public static float carryingSpeed = 0.75f;
    public static float ballSpeed = 1.5f;
    public static float returnSpeed = 2f;
    public static float detectionRange = 35f;

    public static float maxPartEnergy = 1f;

    public static string PlayerField = "player_field";
    public static string EnemyField = "enemy_field";

    public static string statusAttacker = "Attacker";
    public static string statusDefender = "Defender";

    public static bool startTimer = true;
    public static bool fillEnergy = true;

    // public static bool playAR = false;

    public static float NormalSpeedAttacker { get => normalSpeedAttacker * scalingObject; }
    public static float NormalSpeedDefender { get => normalSpeedDefender * scalingObject; }
    public static float CarryingSpeed { get => carryingSpeed * scalingObject; }
    public static float BallSpeed { get => ballSpeed * scalingObject; }
    public static float ReturnSpeed { get => returnSpeed * scalingObject; }

    private static PlayerCondition SetModeAttacker()
    {
        PlayerCondition playerCondition = new PlayerCondition();
        playerCondition.modePlayer = ModePlayer.attacker;
        playerCondition.energyRegeneration = energyRegeneration;
        playerCondition.energyCost = energyCostAttacker;
        playerCondition.spawnTime = spawnTimeAttacker;
        playerCondition.status = statusAttacker;
        return playerCondition;
    }

    private static PlayerCondition SetModeDefender()
    {
        PlayerCondition playerCondition = new PlayerCondition();
        playerCondition.modePlayer = ModePlayer.defender;
        playerCondition.energyRegeneration = energyRegeneration;
        playerCondition.energyCost = energyCostDefender;
        playerCondition.spawnTime = spawnTimeDefender;
        playerCondition.status = statusDefender;
        return playerCondition;
    }

    public static PlayerCondition GetPlayerCondition(ModePlayer modePlayer)
    {
        if (modePlayer == ModePlayer.attacker)
        {
            if (GameManager.Instance.matchRound % 2 == 1)
            {
                return SetModeAttacker();
            }
            else
            {
                return SetModeDefender();
            }
        }
        else
        {
            if (GameManager.Instance.matchRound % 2 == 1)
            {
                return SetModeDefender();
            }
            else
            {
                return SetModeAttacker();
            }
        }
    }
}
