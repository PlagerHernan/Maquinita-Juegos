﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [Header("Paths")]
    [SerializeField] string _sounds = "Audio/SFX";
    [SerializeField] string _music = "Audio/Music";

    [SerializeField] Dictionary<string, AudioClip> _soundList = new Dictionary<string, AudioClip>();
    [SerializeField] Dictionary<string, AudioClip> _musicList = new Dictionary<string, AudioClip>();

    public List<AudioMixerSnapshot> snapshots = new List<AudioMixerSnapshot>();
    public AudioMixer mixer;

    AudioListener _listener;

    private AudioSource src;

    void Awake()
    {
        _soundList = LoadSoundsFromStorage(_sounds);
        _musicList = LoadSoundsFromStorage(_music);

        src = GetComponent<AudioSource>();
        _listener = Camera.main.GetComponent<AudioListener>();

        //SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume", 0.75f));
        //SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 0.75f));
    }

    public void SetAudioSnapshot(int select)
    {
        if (snapshots.Count <= 0)
        {
            Debug.Log("No snapshots loaded");
            return;
        }

        if (snapshots.Count - 1 < select)
        {
            Debug.Log("Insufficient snapshots loaded");
            return;
        }

        snapshots[select].TransitionTo(0f);
            
    }

    Dictionary<string, AudioClip> LoadSoundsFromStorage(string pathInResources)
    {
        AudioClip[] tempAudios = Resources.LoadAll<AudioClip>(pathInResources);
        Dictionary<string, AudioClip> tempDictionary = new Dictionary<string, AudioClip>();

        foreach (var item in tempAudios)
        {
            tempDictionary.Add(item.name, item);
        }

        return tempDictionary;
    }

    public AudioClip GetSound(string sound)
    {
        string[] soundSelection = sound.Split('_');

        if (soundSelection[0] == "m")
            return _musicList[sound];
        else
            return _soundList[sound];
    }

    public void ClickSound(string name)
    {
        src.PlayOneShot(GetSound(name));
    }

    public void SetSFXVolume(float val)
    {
        val = Mathf.Clamp(val, 0.001f, 1);
        mixer.SetFloat("sfxVol", Mathf.Log(val) * 20);
        //PlayerPrefs.SetFloat("SFXVolume", val);
    }
    public void SetMusicVolume(float val)
    {
        val = Mathf.Clamp(val, 0.001f, 1);
        mixer.SetFloat("musicVol", Mathf.Log(val) * 20);
        //PlayerPrefs.SetFloat("MusicVolume", val);
    }
}
