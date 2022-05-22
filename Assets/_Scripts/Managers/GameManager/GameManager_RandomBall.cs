using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
    [Header("RANDOM BALL")]
    [SerializeField] private GameObject ballPrefab;

    public GameObject ball;

    private string SetBattleField()
    {
        if (player.modePlayer == ModePlayer.attacker)
        {
            return Parameters.PlayerField; 
        }
        else
        {
            return  Parameters.EnemyField;
        }
    }

    private void RandomBall()
    {
        string tag = SetBattleField();

        GameObject field = GameObject.FindGameObjectWithTag(tag);
        Vector3[] vertices = field.GetComponent<MeshFilter>().mesh.vertices;
        Vector3 topLeft = field.transform.TransformPoint(vertices[0]);
        Vector3 topRight = field.transform.TransformPoint(vertices[120]);

        float range = .70f;
        topLeft *= range;
        topRight *= range;

        if (ball == null)
        {
            ball = Instantiate(ballPrefab, objectUnit.transform, true);
        }

        Vector3 pos = new Vector3(Random.Range(topLeft.x, topRight.x), field.transform.position.y, Random.Range(topLeft.z, topRight.z));
        ball.transform.position = pos;

        ball.SetActive(true);
    }
}
