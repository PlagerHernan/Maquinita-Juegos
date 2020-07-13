using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    AudioSource _audioSource;
    Manager _manager;

    private void Awake() 
    {
        _audioSource = GetComponent<AudioSource>();
        _manager = FindObjectOfType<Manager>();
    }

    private void Start() 
    {
        SetMusic();    
    }

    public void SetMusic()
    {
        if (_manager.MusicOn)
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
