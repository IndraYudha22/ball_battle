using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSoldier : Soldier
{
    [SerializeField] private GameObject indicatorArea;

    public AttackerSoldier target;
    public bool caughtTarget = false;
    public bool returningToPosition = false;

    private void Awake()
    {
        SetOriginPosition();
    }

    private void OnEnable()
    {
        StartCoroutine(ActivateDetection());
        StartCoroutine(SpawnSoldier(Parameters.spawnTimeDefender));
    }

    private void Update()
    {
        SoldierDefaultDirection();
        MovementSoldier();
    }

    IEnumerator ActivateDetection()
    {
        yield return new WaitForSeconds(Parameters.spawnTimeDefender);
        indicatorArea.SetActive(true);
    }

    IEnumerator ReactivateSoldier()
    {
        yield return new WaitForSeconds(Parameters.reactivateTimeDefender);
        returningToPosition = true;
        caughtTarget = false;
        soldierActive = true;
    }

    private void MovementSoldier()
    {
        if (soldierActive)
        {
            if (target != null)
            {
                animator.Play("running");
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Parameters.NormalSpeedDefender * Time.deltaTime);

                indicatorArea.SetActive(false);

                if (caughtTarget)
                {
                    animator.Play("idle");
                    SetStatusSoldier(false);
                    SetKinematic(true);
                    StartCoroutine(ReactivateSoldier());
                }
                else
                {
                    if (target.isCaught)
                    {
                        SetStatusSoldier(false);
                        SetKinematic(true);
                        returningToPosition = true;
                    }
                }
            }
        }

        if (returningToPosition)
        {
            animator.Play("running");
            transform.position = Vector3.MoveTowards(transform.position, originPosition, Parameters.ReturnSpeed * Time.deltaTime);
            if (transform.position == originPosition)
            {
                returningToPosition = false;
                target = null;
                SetStatusSoldier(true);
                SetKinematic(false);
                indicatorArea.SetActive(true);
            }
        }
    }
}
