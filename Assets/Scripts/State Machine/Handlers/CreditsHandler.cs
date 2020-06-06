using System;
using UnityEngine;

public class CreditsHandler : MonoBehaviour
{
    [SerializeField] GameObject _creditsScreen;
    bool _backToPause;

    public bool BackToPause { get; set; }

    Func<bool> ReturnToPause() => () => BackToPause;

    public void EnterMethods()
    {
        if (_creditsScreen) _creditsScreen.SetActive(true);
        BackToPause = false;
    }
    public void ExitMethods()
    {
        if (_creditsScreen) _creditsScreen.SetActive(false);
    }

    public Func<bool> ReturnConditioner()
    {
        return ReturnToPause();
    }
}
