using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXPlayer : MonoBehaviour
{
    public static SFXPlayer instance;
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
    /// Reproduce un sonido
    /// </summary>
    /// <param name="audio">El clip de audio a reproducir</param>
    public void PlaySound(AudioClip audio)
    {
        if (_src == null) return;

        _src.clip = audio;
        _src.Play();
    }

    /// <summary>
    /// Reproduce un sonido
    /// </summary>
    /// <param name="audioName">Nombre de un clip de audio que se encuentre en la carpeta de recursos</param>
    public void PlaySound(string audioName)
    {
        if (_src == null || _soundManager == null) return;

        _src.clip = _soundManager.GetSound(audioName);
        _src.Play();
    }

    /// <summary>
    /// Devuelve el clip de audio correspondiente al nombre.
    /// </summary>
    /// <param name="name">Nombre del clip</param>
    /// <returns></returns>
    public AudioClip GetSound(string name)
    {
        return _soundManager.GetSound(name);
    }
}
