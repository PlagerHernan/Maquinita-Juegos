using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
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
        SetMusic();    
    }

    public void SetMusic()
    {
        if (_menuManager.MusicOn)
        {
            PlayMusic();
        }
        else
        {
            PauseMusic();
        }
    }

    void PlayMusic()
    {
        _audioSource.Play();
    }
    void PauseMusic()
    {
        _audioSource.Pause();
    }
}
