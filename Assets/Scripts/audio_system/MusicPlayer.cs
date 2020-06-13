using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;
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

    public void ChangeMusic(AudioClip audio)
    {
        if (_src == null) return;

        _src.clip = audio;
        _src.Play();
    }

    public void ChangeMusic(string audioName)
    {
        if (_src == null || _soundManager == null) return;

        _src.clip = _soundManager.GetSound(audioName);
        _src.Play();
    }
}
