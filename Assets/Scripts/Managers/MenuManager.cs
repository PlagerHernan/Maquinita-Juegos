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

    void LoadSettingsInfo()
    {
        _gameSettings = _saveSystem.GetGameSettings(); 
        _musicState = _gameSettings.musicOn;
        _soundState = _gameSettings.soundFXOn;
    }
    void LoadUserInfo()
    {
        _user = _saveSystem.GetUser();
    }

    private void Start() 
    {
        _gameSettings = _saveSystem.GetGameSettings(); 
        _user = _saveSystem.GetUser();    
    }
}