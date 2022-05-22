using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [Header("SOLDIER")]
    public Fractions fraction;
    [SerializeField] private GameObject objectSoldier;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private SO_ColorFraction colorFraction;
    [SerializeField] private GameObject indicatorArrow;

    public Vector3 originPosition;
    public Vector3 lastPosition;

    public bool soldierActive = false;

    public virtual void SetOriginPosition()
    {
        originPosition = transform.position;
        lastPosition = transform.position;
    }

    public virtual void SetStatusSoldier(bool status)
    {
        meshRenderer.material = (!status) ? colorFraction.SetMaterialDisable() : colorFraction.SetMaterial(fraction);
        soldierActive = status;
    }

    public virtual void SetKinematic(bool kinematic)
    {
        Rigidbody rigidbody = objectSoldier.GetComponent<Rigidbody>();

        if (kinematic)
        {
            rigidbody.isKinematic = true;
            rigidbody.detectCollisions = false;
        }
        else
        {
            rigidbody.isKinematic = false;
            rigidbody.detectCollisions = true;
        }
    }

    public virtual IEnumerator SpawnSoldier(float timer)
    {
        SetStatusSoldier(false);
        yield return new WaitForSeconds(timer);
        SetStatusSoldier(true);
    }

    public virtual void SoldierDefaultDirection()
    {
        if (transform.position != lastPosition)
        {
            Vector3 targetDirection = transform.position - lastPosition;
            lastPosition = transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, Parameters.returnSpeed, 0f);

            transform.rotation = Quaternion.LookRotation(newDirection);

            indicatorArrow.SetActive(true);
        }
        else
        {
            indicatorArrow.SetActive(false);
        }
    }
}
