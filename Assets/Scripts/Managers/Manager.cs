﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] protected bool _musicState = true;
    [SerializeField] protected bool _soundState = true;

    //Variables privadas, no son visibles desde el inspector
    protected SaveSystem _saveSystem;

    protected GameSettings _gameSettings;
    protected User _user;
    protected ListAttempts _listAttempts;

    protected bool _lastMusicState;
    protected bool _lastSoundState;

    protected bool _isLastScene;

    //propiedades de Settings
    public bool MusicOn { get => _musicState; set => _musicState = value; }
    public bool SoundOn { get => _soundState; set => _soundState = value; }

    //propiedades de User
    public string UserName { get => _user.name; set => _user.name = value; }
    public int UserLevel { get => _user.currentLevel;}
    public float ExperiencePoints { get => _user.experiencePoints; set => _user.experiencePoints = value; }
    
    float _expPointsAttempt; public float ExpPointsAttempt { get => _expPointsAttempt; set => _expPointsAttempt = value; }
    public bool IsLastScene { get => _isLastScene; }
    

    //public int CurrentLevel { get => _user.currentLevel;}

    virtual protected void Awake()
    {
        _saveSystem = FindObjectOfType<SaveSystem>();
    }


    //cierre forzado
    void OnApplicationQuit() 
    {
        SaveSettingsInfo();
        //SaveUserInfo();
    }

    //Reinicia escena o vuelve al menu
    virtual protected void OnDisable()
    {
        SaveSettingsInfo();
    }

    //Guardo los game settings
    public void SaveSettingsInfo()
    {
        _gameSettings.musicOn = _musicState;
        _gameSettings.soundFXOn = _soundState;

        var oldGameSettings = _saveSystem.GetGameSettings();
        if (!oldGameSettings.Equals(_gameSettings))
            _saveSystem?.SetGameSettings(_gameSettings);
    }
    //Guardo la info de usuario
    public void SaveUserInfo()
    {
        var oldUserData = _saveSystem.GetUser();

        if (!oldUserData.Equals(_user))
            _saveSystem?.SetUser(_user);
    }

    private void NewAttempt(bool result)
    {
        _listAttempts = _saveSystem.GetListAttempts();

        Attempt newAttempt = new Attempt();

        //newAttempt.game_Level = 
        //newAttempt.current_Game_Level = 
        newAttempt.current_User_Level_In_The_Game = UserLevel; //nivel del usuario
        newAttempt.experience_Points_per_Attempt = ExpPointsAttempt; //puntos de exp de la partida
        newAttempt.level_Completed = result; //derrota o victoria

        _listAttempts.list.Add(newAttempt);
        _saveSystem.SetListAttempts(_listAttempts);

        print("Partida guardada");
    }

    //Guardo info de partida en lista de partidas
    public void LevelCompletedAttempt() => NewAttempt(true);
    public void LoseAttempt() => NewAttempt(false);

    //Cargo los gamesettings
    protected void LoadSettingsInfo()
    {
        _gameSettings = _saveSystem.GetGameSettings();
        _musicState = _gameSettings.musicOn;
        _soundState = _gameSettings.soundFXOn;
    }
    //Cargo la info de usuario
    protected void LoadUserInfo()
    {
        _user = _saveSystem.GetUser();
    }
    public void UnlockLevel()
    {
        int sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        if (!IsLastScene && _user.currentLevel <= sceneIndex)
            _user.currentLevel = sceneIndex + 1;
    }
}