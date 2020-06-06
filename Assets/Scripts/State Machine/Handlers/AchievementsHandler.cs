using UnityEngine;

public class AchievementsHandler : MonoBehaviour
{
    [SerializeField] GameObject achievementsScreen;
    private bool _backToPause;

    public bool BackToPause { get => _backToPause; set => _backToPause = value; }

    public void EnterMethods()
    {
        if (achievementsScreen) achievementsScreen.SetActive(true);
        BackToPause = false;
    }
    public void ExitMethods()
    {
        if (achievementsScreen) achievementsScreen.SetActive(false);
    }
}
