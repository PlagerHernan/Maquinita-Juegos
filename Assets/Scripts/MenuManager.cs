using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    SaveSystem _saveSystem;
    GameSettings _gameSettings;
    [SerializeField] bool _musicState = true;
    [SerializeField] bool _soundState = true;
    
    public bool MusicOn { get => _musicState; set => _musicState = value; }
    public bool SoundOn { get => _soundState; set => _soundState = value; } 

    protected void Awake() 
    {
        _saveSystem = FindObjectOfType<SaveSystem>();

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