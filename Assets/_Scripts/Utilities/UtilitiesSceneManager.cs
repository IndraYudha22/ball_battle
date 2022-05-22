using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class UtilitiesSceneManager
{
    private static Dictionary<int, string> scenes = new Dictionary<int, string>()
    {
        {((int)SelectScene.main_menu), "Main Menu"},
        {((int)SelectScene.gameplay), "Gameplay"},
    };

    public static string GetScene(SelectScene selectScene)
    {
        return scenes.Values.ElementAt(((int)selectScene));
    }
}
