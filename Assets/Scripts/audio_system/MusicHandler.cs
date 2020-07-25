using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    AudioLoader _audioLoader;
    AudioSource _audioSource;
    Manager _manager;

    private void Awake() 
    {
        _audioLoader = FindObjectOfType<AudioLoader>();
        _audioSource = GetComponent<AudioSource>();
        _manager = FindObjectOfType<Manager>();
    }

    private void Start() 
    {
        //Carga el estado de la musica desde el manager. Si está desactivado, pausa la musica, sino le da play.
        if (_manager.MusicOn)
            _audioSource.Play();
        else
            _audioSource.Pause();
    }

    //Reproduzco la música
    public void Play()
    {
        _manager.MusicOn = true;
        _audioSource.Play();
    }

    //Pauso la música
    public void Pause()
    {
        _manager.MusicOn = false;
        _audioSource.Pause();
    }

    //Cambio la música
    public void ChangeSong(string name)
    {
        var clip = _audioLoader.GetSound(name);

        _audioSource.clip = clip;
        Play();
    }
}
