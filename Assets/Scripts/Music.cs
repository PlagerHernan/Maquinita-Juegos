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
        if (_menuManager.MusicOff)
        {
            PlayMusic();
            print("Musica: " + _menuManager.MusicOff);
        }
        else
        {
            PauseMusic();
            print("Musica: " + _menuManager.MusicOff);
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
