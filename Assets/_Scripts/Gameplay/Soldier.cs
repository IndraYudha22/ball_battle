using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [Header("SOLDIER")]
    [SerializeField] private Fractions fraction;
    [SerializeField] private GameObject objectSoldier;
    [SerializeField] private float energyCost;
    [SerializeField] private Material material;
    [SerializeField] private SO_ColorFraction colorFraction;

    private Vector3 originPosition;
    private Vector3 lastPosition;

    private bool stopMove;
    private bool inactivateSoldier;

    private Color colorGrey = Color.grey; // color inactive soldier

    public virtual void SetOriginPosition()
    {
        originPosition = transform.parent.position;
        lastPosition = transform.position;
        inactivateSoldier = true;
    }

    public virtual void InactivateSoldier(float time)
    {
        if (inactivateSoldier)
        {
            StartCoroutine(InactivateSoldier(time));

            IEnumerator InactivateSoldier(float time)
            {
                material.color = colorGrey;
                yield return new WaitForSeconds(time);
                material.color = colorFraction.SetColor(fraction);

                inactivateSoldier = false;
                stopMove = false;
            }
        }
    }

    public virtual void SpawnSoldier(float time)
    {
        StartCoroutine(SpawnSoldier(time));

        IEnumerator SpawnSoldier(float time)
        {
            yield return new WaitForSeconds(time);
        }
    }
}
