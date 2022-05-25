using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.PlayerInfo;
using UI.Timer;

public partial class GameManager : StaticInstance<GameManager>
{
    [Header("GAME MANAGER")]
    public UI_PlayerInfo playerInfo;
    public UI_PlayerInfo enemyInfo;

    private PlayerCondition player;
    private PlayerCondition enemy;

    public GameObject soldierAttackerPrefab;
    public GameObject soldierDefenderPrefab;

    public GameObject gatePlayer;
    public GameObject gateEnemy;
    public GameObject battleField;

    [SerializeField] private GameObject objectUnit;
    public List<AttackerSoldier> listAttackerSoldiers;
    public List<DefenderSoldier> listDefenderSoldiers;

    public Soldier ballHolder;

    private GameObject ballObj;

    public delegate void SetPlayerCondition();
    public event SetPlayerCondition setPlayerCondition;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        SetModePlayer(); // set mode player in game

        PlayerClick(); // set controller condition onPlayer
        EnemyClick(); // set acontroller condition onEnemy

        Draw(); // set draw condition

        // if (!Parameters.playAR)
        // {
        //     RandomBall(); // random ball first time
        // }

        RandomBall(); // random ball first time
        
        setPlayerCondition(); // set subscribe from another class
    }

    private void SetModePlayer()
    {
        player = Parameters.GetPlayerCondition(ModePlayer.attacker);
        enemy = Parameters.GetPlayerCondition(ModePlayer.defender);
    }

    public PlayerCondition GetModePlayer()
    {
        return player;
    }

    public void InitialGame()
    {
        Parameters.startTimer = true;
        Parameters.fillEnergy = true;

        listAttackerSoldiers.Clear();
        listDefenderSoldiers.Clear();

        player = Parameters.GetPlayerCondition(ModePlayer.attacker);
        enemy = Parameters.GetPlayerCondition(ModePlayer.defender);

        setPlayerCondition();

        RandomBall();

        UIManager.Instance.ShowCanvas(SelectCanvas.gameplay);
    }


    public void PlayerClick()
    {
        var clickHandler = PlayerController.Instance;

        clickHandler.delegateOnClickPlayer = (RaycastHit hit) =>
        {
            if (playerInfo.GetCurrentEnergy(Fractions.player) > player.energyCost)
            {
                playerInfo.DecreaseEnergy(Fractions.player, player.energyCost);

                SpawnSoldierAttacker(hit);
            }

        };
    }

    public void EnemyClick()
    {
        var clickHandler = PlayerController.Instance;

        clickHandler.delegateOnClickEnemy = (RaycastHit hit) =>
        {
            if (enemyInfo.GetCurrentEnergy(Fractions.enemy) > enemy.energyCost)
            {
                enemyInfo.DecreaseEnergy(Fractions.enemy, enemy.energyCost);

                SpawnSoldierDefender(hit);
            }

        };
    }

    private void SpawnSoldierAttacker(RaycastHit hit)
    {
        if (player.modePlayer == ModePlayer.attacker)
        {
            GameObject soldier = Instantiate(soldierAttackerPrefab, hit.point, Quaternion.identity, objectUnit.transform);
            AttackerSoldier attacker = soldier.GetComponent<AttackerSoldier>();
            attacker.fraction = Fractions.player;
            attacker.ball = ball;
            listAttackerSoldiers.Add(attacker);
        }
        else
        {
            GameObject soldier = Instantiate(soldierDefenderPrefab, hit.point, Quaternion.identity, objectUnit.transform);
            DefenderSoldier defender = soldier.GetComponent<DefenderSoldier>();
            defender.fraction = Fractions.player;
            listDefenderSoldiers.Add(defender);
        }
    }

    private void SpawnSoldierDefender(RaycastHit hit)
    {
        if (enemy.modePlayer == ModePlayer.attacker)
        {
            GameObject soldier = Instantiate(soldierAttackerPrefab, hit.point, Quaternion.Euler(0, 180, 0), objectUnit.transform);
            AttackerSoldier attacker = soldier.GetComponent<AttackerSoldier>();
            attacker.fraction = Fractions.enemy;
            attacker.ball = ball;
            listAttackerSoldiers.Add(attacker);
        }
        else
        {
            GameObject soldier = Instantiate(soldierDefenderPrefab, hit.point, Quaternion.Euler(0, 180, 0), objectUnit.transform);
            DefenderSoldier defender = soldier.GetComponent<DefenderSoldier>();
            defender.fraction = Fractions.enemy;
            listDefenderSoldiers.Add(defender);
        }
    }
}
