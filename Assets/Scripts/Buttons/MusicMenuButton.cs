using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MusicMenuButton : MonoBehaviour, IPointerUpHandler
{
    Manager _manager;
    Music _music;

    Toggle _musicToggle;

    private void Awake() 
    {
        _manager = FindObjectOfType<Manager>();
        _music = FindObjectOfType<Music>();

        _musicToggle = GetComponent<Toggle>();
    }

    private void Start() 
    {
        print("_menuManager.MusicOn: " + _manager.MusicOn);
        _musicToggle.isOn = !_manager.MusicOn;
    }

    public void OnPointerUp (PointerEventData evenData)
	{
        _manager.MusicOn = _musicToggle.isOn;
        _music.SetMusic();
	}
}
