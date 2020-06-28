using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    MenuManager _menuManager;
    Toggle _musicToggle;
    Toggle _soundToggle;

    private void Awake() 
    {
        #region Inicialización de variables

        _menuManager = GameObject.FindObjectOfType<MenuManager>();

        _musicToggle = GameObject.Find("Music Button").GetComponent<Toggle>();
        _soundToggle = GameObject.Find("Sound Button").GetComponent<Toggle>();

        #endregion

        _musicToggle.isOn = _menuManager.GameSettings.musicOn;
        _soundToggle.isOn = _menuManager.GameSettings.soundFXOn;
    }

	//llamado desde Back Button 
	public void SaveSettingsInfo()
    {
		/* _gameSettings.musicOn = _musicToggle.isOn;
		_gameSettings.soundFXOn = _soundToggle.isOn;

		print("Musica: " + _musicToggle.isOn);
		print("Sonido: " + _soundToggle.isOn);

		_saveSystem.SetGameSettings(_gameSettings); */
    }
}
