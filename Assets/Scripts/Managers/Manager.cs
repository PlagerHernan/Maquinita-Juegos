using UnityEngine;
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
    public Language Language { get => _gameSettings.language; set => _gameSettings.language = value; }

    //propiedades de User
    public string UserName { get => _user.name; set => _user.name = value; }
    public int UserLevel { get => _user.currentLevel;}
    public float ExperiencePoints { get => _user.experiencePoints; set => _user.experiencePoints = value; }

    private bool isLastScene; public bool IsLastScene { get => isLastScene; set => isLastScene = value; }

    //public int CurrentLevel { get => _user.currentLevel;}

    virtual protected void Awake()
    {
        _saveSystem = FindObjectOfType<SaveSystem>();
    }


    //cierre forzado
    virtual protected void OnApplicationQuit() 
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

        //si no es la última escena y el siguiente nivel no está aún desbloqueado, lo desbloquea
        if (!IsLastScene && _user.currentLevel <= sceneIndex)
            _user.currentLevel = sceneIndex + 1;
    }
}