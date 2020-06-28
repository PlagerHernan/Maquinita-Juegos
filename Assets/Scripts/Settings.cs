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

        _musicToggle.isOn = _menuManager.MusicOff;
        _soundToggle.isOn = _menuManager.SoundOff;
    }

	public void SetMusic()
    {
		_menuManager.MusicOff = _musicToggle.isOn;
		_menuManager.SoundOff = _soundToggle.isOn;

		print("Musica: " + _musicToggle.isOn);
		print("Sonido: " + _soundToggle.isOn);
    }
}
