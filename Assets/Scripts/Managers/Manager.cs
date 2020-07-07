﻿using UnityEngine;

public class Manager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] protected bool _musicState = true;
    [SerializeField] protected bool _soundState = true;

    protected SoundManager _soundManager;
    protected SaveSystem _saveSystem;
    protected GameSettings _gameSettings;
    protected User _user;

    //Variables privadas, no son visibles desde el inspector
    protected bool _lastMusicState;
    protected bool _lastSoundState;
    protected bool _isLastScene;
    protected float _baseTime;
    protected float _gameTime;

    public bool MusicOn { get => _musicState; set => _musicState = value; }
    public bool SoundOn { get => _soundState; set => _soundState = value; }
    public float ExperiencePoints { get => _user.experiencePoints; set => _user.experiencePoints = value; }
    public bool IsLastScene { get => _isLastScene; }

    virtual protected void Awake()
    {
        _saveSystem = FindObjectOfType<SaveSystem>();
    }
}