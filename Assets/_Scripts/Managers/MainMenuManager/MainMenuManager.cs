using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Android;
using UnityEngine.UI;

public class MainMenuManager : StaticInstance<MainMenuManager>
{
    [SerializeField] private Button btnPlayAR;

    protected override void Awake()
    {
        base.Awake();
        EnableGameplayAR();
    }

    private void EnableGameplayAR()
    {   
        if (Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            btnPlayAR.interactable = true; // set enable button play AR
            ParametersMenu.SetPlayerPrefsAR(1);
        }
        else
        {
            btnPlayAR.interactable = false; // set disable button play AR
            ParametersMenu.SetPlayerPrefsAR(0);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(UtilitiesSceneManager.GetScene(SelectScene.gameplay));
    }

    public void PlayGameAR()
    {
        SceneManager.LoadScene(UtilitiesSceneManager.GetScene(SelectScene.gameplayAR));
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
