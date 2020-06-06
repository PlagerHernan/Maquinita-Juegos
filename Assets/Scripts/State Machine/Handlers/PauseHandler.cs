using System;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    private bool _resume = false;
    private bool _watchAchievements = false;
    private bool _pauseToCredits = false;

    public bool Resume { get; set ; }
    public bool WatchAchievements { get => _watchAchievements; set => _watchAchievements = value; }
    public bool PauseToCredits { get; set; }

    Func<bool> GoFromPauseToCredits() => () => PauseToCredits;

    public void EnterMethods()
    {
        if (pauseScreen) pauseScreen.SetActive(true);
        Resume = false;
        WatchAchievements = false;
        PauseToCredits = false;
    }
    public void ExitMethods()
    {
        if (pauseScreen) pauseScreen.SetActive(false);
    }

    public Func<bool> ReturnConditioner()
    {
        return GoFromPauseToCredits();
    }
}
