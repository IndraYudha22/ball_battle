using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSoldier : Soldier
{
    private void Awake()
    {

    }

    private void Start()
    {
        SetOriginPosition();
        InactivateSoldier(Parameters.spawnTimeAttacker);
    }

    public override void InactivateSoldier(float time)
    {
        base.InactivateSoldier(time);
    }

    public override void SetOriginPosition()
    {
        base.SetOriginPosition();
    }

    public override void SpawnSoldier(float time)
    {
        base.SpawnSoldier(time);
    }
}
