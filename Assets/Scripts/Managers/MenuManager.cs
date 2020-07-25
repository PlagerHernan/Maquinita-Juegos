using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Manager
{
    override protected void Awake()
    {
        base.Awake();
    }

    private void Start() 
    {
        //_gameSettings = _saveSystem.GetGameSettings(); 
        LoadSettingsInfo();
        LoadUserInfo();
        //_user = _saveSystem.GetUser();    
    }

    private void SubscribeEvents()
    {
        EventsHandler.SubscribeToEvent("GP_MAINMENU", SaveSettingsInfo);
    }

    #region OnClickEvents

    public void OnClickMenuScreen() => EventsHandler.TriggerEvent("GP_MAINMENU");
    public void OnClickSettingsScreen() => EventsHandler.TriggerEvent("GP_SETTINGS");
    public void OnClickCreditsScreen() => EventsHandler.TriggerEvent("GP_CREDITS");
    public void OnClickLevelSelectScreen() => EventsHandler.TriggerEvent("GP_LEVELSELECT");

    #endregion


}