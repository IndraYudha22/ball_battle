using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
