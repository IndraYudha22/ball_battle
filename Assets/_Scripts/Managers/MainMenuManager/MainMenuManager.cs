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
        SendRequestPermissionCamera();
    }

    private void Update()
    {
        if (ParametersMenu.GetPlayerPrefsAR() == 1) return;
        if (Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            btnPlayAR.interactable = true;
            ParametersMenu.SetPlayerPrefsAR(1);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(UtilitiesSceneManager.GetScene(SelectScene.gameplay));
    }

    public void PlayGameAR()
    {
        SceneManager.LoadScene(UtilitiesSceneManager.GetScene(SelectScene.gameplayAR));
        // Parameters.playAR = true;
    }

    public void Menu()
    {
        UIManager.Instance.ShowCanvas(SelectCanvas.menu);
    }

    public void Quit()
    {
        Application.Quit();
    }

    #region PERMISSION
    internal void PermissionCallbacks_PermissionDeniedAndDontAskAgain(string permissionName)
    {
        btnPlayAR.interactable = false;
        ParametersMenu.SetPlayerPrefsAR(0);
    }

    internal void PermissionCallbacks_PermissionGranted(string permissionName)
    {
        btnPlayAR.interactable = true;
        ParametersMenu.SetPlayerPrefsAR(1);
    }

    internal void PermissionCallbacks_PermissionDenied(string permissionName)
    {
        btnPlayAR.interactable = false;
        ParametersMenu.SetPlayerPrefsAR(0);
    }

    private void SendRequestPermissionCamera()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            btnPlayAR.interactable = true;
            ParametersMenu.SetPlayerPrefsAR(1);
        }
        else
        {
            bool useCallbacks = false;
            if (!useCallbacks)
            {
                Permission.RequestUserPermission(Permission.Camera);
            }
            else
            {
                var callbacks = new PermissionCallbacks();
                
                callbacks.PermissionDenied += PermissionCallbacks_PermissionDenied;
                callbacks.PermissionGranted += PermissionCallbacks_PermissionGranted;
                callbacks.PermissionDeniedAndDontAskAgain += PermissionCallbacks_PermissionDeniedAndDontAskAgain;

                Permission.RequestUserPermission(Permission.Camera, callbacks);
            }
        }
    }
    #endregion
}
