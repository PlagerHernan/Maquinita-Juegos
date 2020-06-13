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

    public void PlaySound(AudioClip audio)
    {
        if (_src == null) return;

        _src.clip = audio;
        _src.Play();
    }

    public void PlaySound(string audioName)
    {
        //src.clip = soundManager.GetSound(audioName);
        //src.Play();
        if (_src == null || _soundManager == null) return;

        _src.clip = _soundManager.GetSound(audioName);
        _src.Play();
    }

    public AudioClip GetSound(string name)
    {
        return _soundManager.GetSound(name);
    }
}
