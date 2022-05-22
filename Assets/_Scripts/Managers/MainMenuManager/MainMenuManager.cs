using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : StaticInstance<MainMenuManager>
{
    public void PlayGame()
    {
        SceneManager.LoadScene(UtilitiesSceneManager.GetScene(SelectScene.gameplay));
    }

    public void Menu()
    {
        UIManager.Instance.ShowCanvas(SelectCanvas.menu);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
