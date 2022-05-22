using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDetection : MonoBehaviour
{
    public DefenderSoldier defenderSoldier;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "atk_soldier")
        {
            AttackerSoldier soldier = other.transform.parent.GetComponent<AttackerSoldier>();
            if (GameManager.Instance.ballHolder == soldier)
            {
                defenderSoldier.target = soldier;
            }
        }
    }
}
