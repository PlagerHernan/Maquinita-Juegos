using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Manager
{
    override protected void Awake()
    {
        base.Awake();

        SubscribeEvents();

        LoadSettingsInfo();
        LoadUserInfo();
    }

    private void Start() 
    {
        _gameSettings = _saveSystem.GetGameSettings(); 
        _user = _saveSystem.GetUser();    
    }

    private void SubscribeEvents()
    {
        EventsManager.SubscribeToEvent("GP_MAINMENU", SaveSettingsInfo);
    }

    #region OnClickEvents

    public void OnClickMenuScreen() => EventsManager.TriggerEvent("GP_MAINMENU");
    public void OnClickSettingsScreen() => EventsManager.TriggerEvent("GP_SETTINGS");
    public void OnClickCreditsScreen() => EventsManager.TriggerEvent("GP_CREDITS");
    public void OnClickLevelSelectScreen() => EventsManager.TriggerEvent("GP_LEVELSELECT");

    #endregion


}