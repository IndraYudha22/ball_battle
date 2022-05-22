using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.PlayerInfo;

public class PlayerController : StaticInstance<PlayerController>
{
    private string PlayerField = Parameters.PlayerField;
    private string EnemyField = Parameters.EnemyField;

    public delegate void DelegateOnClickPlayer(RaycastHit hit);
    public DelegateOnClickPlayer delegateOnClickPlayer;

    public delegate void DelegateOnClickEnemy(RaycastHit hit);
    public DelegateOnClickEnemy delegateOnClickEnemy;

    private void Update() 
    {
        OnClick();
    }

    private void ClickHandler(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == PlayerField)
            {
                delegateOnClickPlayer(hit);
            }
            else if (hit.transform.tag == EnemyField)
            {
                delegateOnClickEnemy(hit);
            }
            
        }
    }

    private void OnClick()
    {
        #if UNITY_ANDROID
        Touch[] touches = Input.touches;

        if (touches.Length > 0)
        {
            Touch touch = touches[0];
            if (touch.phase == TouchPhase.Ended)
            {
                ClickHandler(touch.position);
            }
        }
        #endif

        #if UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0))
        {
            ClickHandler(Input.mousePosition);
        }
        #endif
    }
}
