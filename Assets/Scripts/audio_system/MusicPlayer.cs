using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reproductor de musica. El SoundManager debe estar en escena para que funcione.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;
    SoundManager _soundManager;
    AudioSource _src;

    void Awake()
    {
        //DDOL
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        _soundManager = FindObjectOfType<SoundManager>();
        _src = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Cambia la musica del juego
    /// </summary>
    /// <param name="audio">Clip de musica a reproducir</param>
    public void ChangeMusic(AudioClip audio)
    {
        if (_src == null) return;

        _src.clip = audio;
        _src.Play();
    }

    /// <summary>
    /// Cambia la música del juego
    /// </summary>
    /// <param name="audioName">Nombre de un clip de musica que se encuentra en la carpeta de recursos</param>
    public void ChangeMusic(string audioName)
    {
        if (_src == null || _soundManager == null) return;

        _src.clip = _soundManager.GetSound(audioName);
        _src.Play();
    }
}
