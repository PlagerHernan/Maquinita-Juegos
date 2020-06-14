using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("InGame Settings")]
    [SerializeField] GameObject _inGame;
    [Header("Pause Settings")]
    [SerializeField] GameObject _pauseScreen;
    [Header("Menu Scene")]
    [SerializeField] int _menuSceneIndex;

    [Header("Sound Settings")]
    [SerializeField] bool _musicState = true;
    [SerializeField] bool _soundState = true;
    bool _lastMusicState;
    bool _lastSoundState;
    SoundManager _soundManager;

    SaveSystem _saveSystem;
    GameSettings _gameSettings;

    public bool MusicOff { get => !_musicState; set => _musicState = !value; }
    public bool SoundOff { get => !_soundState; set => _soundState = !value; }

    //bool _returnToMenu = false;
    //public bool ReturnToMenu { get ; set; }

    private void OnValidate()
    {
        _menuSceneIndex = Mathf.Clamp(_menuSceneIndex, 0, SceneManager.sceneCount);
    }

    private void Awake()
    {
        _soundManager = FindObjectOfType<SoundManager>();
        _saveSystem = FindObjectOfType<SaveSystem>();

        SubscribeMethodsToEventsManager();
    }

    private void Start()
    {
        LoadSettings();
        _lastMusicState = _musicState;
        _lastSoundState = _soundState;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _pauseScreen != null)
        {
            EventsManager.TriggerEvent("GP_PAUSE");
        }

        if (_musicState != _lastMusicState)
        {
            _lastMusicState = _musicState;
            SetMusicState();
        }
        if (_soundState != _lastSoundState)
        {
            _lastSoundState = _soundState;
            SetSFXState();
        }
    }

    //=============== PRIVATE METHDOS =======================
    private void SubscribeMethodsToEventsManager()
    {
        EventsManager.SubscribeToEvent("GP_MAIN_MENU", LoadMenuScene);
        EventsManager.SubscribeToEvent("GP_PAUSE", Pause);
        EventsManager.SubscribeToEvent("GP_RESUME", Resume);
        EventsManager.SubscribeToEvent("GP_RESUME", SaveSettings);
    }
    private void SetMusicState()
    {
        Debug.Log(_musicState);
        if (_soundManager == null) return;

        if (_musicState) _soundManager.SetMusicVolume(1);
        else _soundManager.SetMusicVolume(0);

        _gameSettings.musicOn = _musicState;

        Debug.Log(_musicState + " | " + _lastMusicState);
    }
    private void SetSFXState()
    {
        if (_soundManager == null) return;

        if (_soundState) _soundManager.SetSFXVolume(1);
        else _soundManager.SetSFXVolume(0);

        _gameSettings.soundFXOn = _soundState;
    }

    //=============== ONCLICK EVENTS ========================

    public void OnClickMainMenu()
    {
        ClickSound();
        EventsManager.TriggerEvent("GP_MAIN_MENU");
    }
    public void OnClickPause()
    {
        ClickSound();
        EventsManager.TriggerEvent("GP_PAUSE");
    }
    public void OnClickResume()
    {
        ClickSound();
        EventsManager.TriggerEvent("GP_RESUME");
    }

    public void ClickSound() => SFXPlayer.instance.PlaySound("s_click");

    //========= EVENTS HANDLED BY EVENTSMANAGER ============

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(_menuSceneIndex);
    }

    public void Pause()
    {
        _pauseScreen.SetActive(true);
        _inGame.SetActive(false);
    }

    public void Resume()
    {
        _inGame.SetActive(true);
        _pauseScreen.SetActive(false);
    }

    public void LoadSettings()
    {
        _gameSettings = _saveSystem.GetGameSettings();

        _musicState = _gameSettings.musicOn;
        _soundState = _gameSettings.soundFXOn;

        SetMusicState();
        SetSFXState();
    }
    public void SaveSettings()
    {
        var oldGameSettings = _saveSystem.GetGameSettings();

        if(!oldGameSettings.Equals(_gameSettings))
            _saveSystem.SetGameSettings(_gameSettings);
    }
}
