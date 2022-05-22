using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Round : StaticInstance<UI_Round>
{
    [SerializeField] private TextMeshProUGUI textRound;
    [SerializeField] private TextMeshProUGUI textPlayerScore;
    [SerializeField] private TextMeshProUGUI textEnemyScore;
    [SerializeField] private Button buttonNext;
    [SerializeField] private Button buttonExit;

    private string statusWin;

    private void OnEnable()
    {
        Result();
    }

    private void Start()
    {
        buttonNext.onClick.AddListener(() =>
        {
            if (statusWin == "")
            {
                GameManager.Instance.NextRound();
                return;
            }
            GameManager.Instance.Restart();
        });
        buttonExit.onClick.AddListener(GameManager.Instance.Exit);
    }

    private void Result()
    {
        GameManager.Instance.resultGame = (int playerScore, int enemyScore, int round, string statusWin) =>
        {
            textRound.text = (statusWin == "") ? $"ROUND {round}" : statusWin;
            textPlayerScore.text = playerScore.ToString();
            textEnemyScore.text = enemyScore.ToString();

            this.statusWin = statusWin;
            buttonNext.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (statusWin == "") ? "NEXT" : "RESTART";
        };
    }
}
