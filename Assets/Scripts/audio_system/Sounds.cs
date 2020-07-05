using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    AudioSource _audioSource;
    MenuManager _menuManager;

    private void Awake() 
    {
        _audioSource = GetComponent<AudioSource>();
        _menuManager = FindObjectOfType<MenuManager>();
    }

    private void Start() 
    {
        SetSound();    
    }

    public void SetSound()
    {
        if (_menuManager.SoundOn)
        {
            _audioSource.mute = false;
        }
        else
        {
            _audioSource.mute = true;
        }
    }
}
