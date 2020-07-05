using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SoundMenuButton : MonoBehaviour, IPointerUpHandler
{
    MenuManager _menuManager;
    Sounds _sounds;

    Toggle _soundToggle;

    private void Awake() 
    {
        _menuManager = FindObjectOfType<MenuManager>();
        _sounds = FindObjectOfType<Sounds>();

        _soundToggle = GetComponent<Toggle>();
    }

    private void Start() 
    {
        _soundToggle.isOn = !_menuManager.SoundOn;
    }

    public void OnPointerUp (PointerEventData evenData)
	{
        _menuManager.SoundOn = _soundToggle.isOn;
        _sounds.SetSound();
	}
}
