using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float energyRegeneration;
    public float energyCost;
    public float spawnTime;
    public float reactiveTime;
    public float normalSpeed;
    public float carryingSpeed;
    public float ballSpeed;
    public float returnSpeed;
    public float detectionRange;
    public int matchPerGame;
    public float timeLimit;
    public float maxPartEnergy;

    protected override void Awake()
    {
        base.Awake();
    }
}
