using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Manager
{
    override protected void Awake()
    {
        base.Awake();
        _gameSettings = _saveSystem.GetGameSettings(); 
        LoadSettingsInfo();
    }

    void LoadSettingsInfo()
    {
        _musicState = _gameSettings.musicOn;
        _soundState = _gameSettings.soundFXOn;
    }

    public void SaveSettingsInfo()
    {
        _gameSettings.musicOn = _musicState;
        _gameSettings.soundFXOn = _soundState;
		_saveSystem.SetGameSettings(_gameSettings); 
    }

    //cierre forzado
    private void OnApplicationQuit() 
    {
        SaveSettingsInfo();
    }
}