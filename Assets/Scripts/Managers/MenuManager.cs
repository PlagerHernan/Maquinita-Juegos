using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Manager
{
    override protected void Awake()
    {
        base.Awake();
        LoadSettingsInfo();
        LoadUserInfo();
    }

    private void Start() 
    {
        _gameSettings = _saveSystem.GetGameSettings(); 
        _user = _saveSystem.GetUser();    
    }

    #region OnClickEvents

    public void OnClickMenuScreen() => EventsManager.TriggerEvent("GP_MAINMENU");
    public void OnClickSettingsScreen() => EventsManager.TriggerEvent("GP_SETTINGS");
    public void OnClickCreditsScreen() => EventsManager.TriggerEvent("GP_CREDITS");
    public void OnClickLevelSelectScreen() => EventsManager.TriggerEvent("GP_LEVELSELECT");

    #endregion


}