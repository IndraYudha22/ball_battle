using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARPlay : StaticInstance<ARPlay>
{
    public GameObject canvas;
    public GameObject gameManager;

    public void PlayGame()
    {
        canvas.SetActive(true);
        gameManager.SetActive(true);
    }
}