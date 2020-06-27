using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    SaveSystem _saveSystem;
    GameSettings _gameSettings;
    Toggle _musicToggle;
    Toggle _soundToggle;

    private void Awake() 
    {
        #region Inicialización de variables
            
        _saveSystem = FindObjectOfType<SaveSystem>();
		_gameSettings = _saveSystem.GetGameSettings(); 

        _musicToggle = GameObject.Find("Music Button").GetComponent<Toggle>();
        _soundToggle = GameObject.Find("Sound Button").GetComponent<Toggle>();

        #endregion

        _musicToggle.isOn = _gameSettings.musicOn;
        _soundToggle.isOn = _gameSettings.soundFXOn;
    }

	//llamado desde Back Button 
	public void SaveSettingsInfo()
    {
		_gameSettings.musicOn = _musicToggle.isOn;
		_gameSettings.soundFXOn = _soundToggle.isOn;

		print("Musica: " + _musicToggle.isOn);
		print("Sonido: " + _soundToggle.isOn);

		_saveSystem.SetGameSettings(_gameSettings);
    }
}
