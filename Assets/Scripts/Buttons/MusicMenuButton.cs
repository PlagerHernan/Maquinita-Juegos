using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MusicMenuButton : MonoBehaviour, IPointerUpHandler
{
    MenuManager _menuManager;
    Music _music;

    Toggle _musicToggle;

    private void Awake() 
    {
        _menuManager = FindObjectOfType<MenuManager>();
        _music = FindObjectOfType<Music>();

        _musicToggle = GetComponent<Toggle>();
    }

    private void Start() 
    {
        _musicToggle.isOn = !_menuManager.MusicOff;
    }

    public void OnPointerUp (PointerEventData evenData)
	{
        _menuManager.MusicOff = _musicToggle.isOn;
        _music.SetMusic();
	}
}
