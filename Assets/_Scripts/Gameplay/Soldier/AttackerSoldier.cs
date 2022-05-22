using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSoldier : Soldier
{
    [SerializeField] private GameObject indicatorHoldingBall;

    public GameObject ball;
    public bool isCaught = false;

    private Vector3 gatePosition;

    private void Awake()
    {
        SetOriginPosition(); // set original position
        SetGatePosition(); // identify goalposts 
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnSoldier(Parameters.spawnTimeAttacker));
    }

    private void Update()
    {
        SoldierDefaultDirection();
        SoldierMovement();
    }

    private void SetGatePosition()
    {
        var gameManager = GameManager.Instance;

        if (gameManager.GetModePlayer().modePlayer == ModePlayer.attacker)
        {
            gatePosition = gameManager.gateEnemy.transform.position;
        }
        else
        {
            gatePosition = gameManager.gatePlayer.transform.position;
        }
    }

    private AttackerSoldier FindAnotherAttackerSoldier()
    {
        float minDistance = float.PositiveInfinity;
        AttackerSoldier anotherAttacker = null;
        foreach (AttackerSoldier attacker in GameManager.Instance.listAttackerSoldiers)
        {
            if (!attacker.soldierActive || attacker == this) continue;
            float distance = Vector3.Distance(attacker.transform.position, transform.position);
            if (distance < minDistance)
            {
                anotherAttacker = attacker;
                minDistance = distance;
            }
        }
        return anotherAttacker;
    }

    private void SoldierMovement()
    {
        if (soldierActive)
        {
            if (GameManager.Instance.ballHolder == null)
            {
                SetKinematic(true);
                transform.position = Vector3.MoveTowards(transform.position, ball.transform.position, Parameters.normalSpeedAttacker * Time.deltaTime);
                if (Vector3.Distance(transform.position, ball.transform.position) <= 0)
                {
                    GameManager.Instance.ballHolder = this;
                    ball.transform.parent = this.transform;
                }
            }
            else if (GameManager.Instance.ballHolder == this)
            {
                if (isCaught)
                {
                    AttackerSoldier findAttacker = FindAnotherAttackerSoldier();
                    if (findAttacker == null)
                    {
                        // end game
                        SetStatusSoldier(false);
                        GameManager.Instance.ResultMatch((fraction == Fractions.player) ? Fractions.enemy : Fractions.player);
                        return;
                    }

                    Vector3 targetDirection = findAttacker.transform.position - transform.position;
                    Vector3 newDiderction = Vector3.RotateTowards(transform.forward, targetDirection, 360, 0f);
                    transform.rotation = Quaternion.LookRotation(newDiderction);

                    GameManager.Instance.ballHolder = null;

                    indicatorHoldingBall.SetActive(false);
                    SetStatusSoldier(false);

                    SetKinematic(true);

                    ball.transform.parent = null;
                    ball.GetComponent<Ball>().passedTo = findAttacker;

                    StartCoroutine(ReactivateSoldier());
                }
                else
                {
                    indicatorHoldingBall.SetActive(true);
                    SetKinematic(false);
                    transform.position = Vector3.MoveTowards(transform.position, gatePosition, Parameters.carryingSpeed * Time.deltaTime);
                }
            }
            else
            {
                SetKinematic(true);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, gatePosition.y, gatePosition.z), Parameters.normalSpeedAttacker * Time.deltaTime);

                if (transform.position.z == gatePosition.z)
                {
                    GameManager.Instance.listAttackerSoldiers.Remove(this);
                    SetStatusSoldier(false);
                    Destroy(gameObject);
                }
            }
        }
    }

    IEnumerator ReactivateSoldier()
    {
        yield return new WaitForSeconds(Parameters.reactivateTimeAttacker);
        SetStatusSoldier(true);
        SetKinematic(true);
        isCaught = false;
    }
}
