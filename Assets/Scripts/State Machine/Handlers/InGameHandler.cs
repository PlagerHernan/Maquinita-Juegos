using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameHandler : MonoBehaviour
{
    [SerializeField] GameObject inGameScreen;
    private bool _paused = false;

    public bool Paused { get; set; }

    public void EnterMethods()
    {
        if (inGameScreen) inGameScreen.SetActive(true);
        Paused = false;
    }
    public void ExitMethods()
    {
        if (inGameScreen) inGameScreen.SetActive(false);
    }
}
