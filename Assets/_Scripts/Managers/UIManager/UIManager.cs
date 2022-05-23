using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : StaticInstance<UIManager>
{
    [Header("CANVASES")]
    [SerializeField] private List<CanvasView> canvasView;

    public void ShowCanvas(SelectCanvas canvasEnum)
    {
        for (int i = 0; i < canvasView.Count; i++)
        {
            if (canvasView[i].selectCanvas == canvasEnum)
            {
                canvasView[i].canvasView.SetActive(true);

                for (int j = 0; j < canvasView[i].inactivateCanvases.Count; j++)
                {
                    canvasView[i].inactivateCanvases[j].SetActive(false);
                }
            }
        }
    }
}

[System.Serializable]
public class CanvasView
{
    public string identity;
    public SelectCanvas selectCanvas;
    public GameObject canvasView;
    public List<GameObject> inactivateCanvases;
}

