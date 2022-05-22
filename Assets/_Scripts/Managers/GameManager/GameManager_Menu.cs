using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager
{
    public void NextRound()
    {
        InitialGame();
    }

    public void Exit()
    {
        SceneManager.LoadScene(UtilitiesSceneManager.GetScene(SelectScene.main_menu));
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Parameters.fillEnergy = true;
        Parameters.startTimer = true;

        SceneManager.LoadScene(UtilitiesSceneManager.GetScene(SelectScene.gameplay));
    }
}
