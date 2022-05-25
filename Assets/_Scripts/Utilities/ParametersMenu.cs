using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ParametersMenu
{
    public static string statusAR = "STATUS_AR";

    // 0 for disable, 1 for enable 
    public static void SetPlayerPrefsAR(int valueStatus)
    {
        PlayerPrefs.SetInt(statusAR, valueStatus);
    }

    public static void GetPlayerPrefsAR()
    {
        PlayerPrefs.GetInt(statusAR);
    }
}
