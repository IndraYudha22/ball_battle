using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolderDetection : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (gameObject.tag == "dfn_soldier" && other.gameObject.tag == "atk_soldier")
        {
            DefenderSoldier defenderSoldier = transform.parent.GetComponent<DefenderSoldier>();
            AttackerSoldier attackerSoldier = other.transform.parent.GetComponent<AttackerSoldier>();

            Debug.Log($"SOLDIER DETECTION : {attackerSoldier.gameObject.name}");

            if (transform.parent.GetComponent<DefenderSoldier>().target == attackerSoldier)
            {
                defenderSoldier.caughtTarget = true;
                attackerSoldier.isCaught = true;

                Debug.Log("SOLDIER MODIFY");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "gate")
        {
            // match over
            if (GameManager.Instance.ballHolder == transform.parent.gameObject.GetComponent<AttackerSoldier>())
            {
                GameManager.Instance.ResultMatch(transform.parent.gameObject.GetComponent<AttackerSoldier>().fraction);
            }
        }
    }
}
