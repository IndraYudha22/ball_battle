using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int matchPerGame = 5;
    public float timeLimit = 140f;
    public float maxEnergyBar = 6f;

    public float energyRegeneration = .5f;

    public float energyCostAttacker = 2;
    public float energyCostDefender = 3;

    public float spawnTimeAttacker = .5f;
    public float spawnTimerDefender = .5f;

    public float reactiveTimeAttacker = 2.5f;
    public float reactiveTimerDefender = 4f;

    public float normalSpeedAttacker = 1.5f;
    public float normalSpeedDefender = 1f;

    public float carryingSpeed = 0.75f;
    public float ballSpeed = 1.5f;
    public float returnSpeed = 2f;
    public float detectionRange = 35f;

    public float maxPartEnergy = 1f;
    

    protected override void Awake()
    {
        base.Awake();
    }
}
