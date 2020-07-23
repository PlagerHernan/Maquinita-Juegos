using UnityEngine;

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
    protected int _countAttempts; //simula ID de attempt

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
        print("Nueva partida agregada");

        _countAttempts++;

        _listAttempts = _saveSystem.GetListAttempts();

        Attempt newAttempt = new Attempt();

        newAttempt.ID_Attempt = _countAttempts;
        newAttempt.current_Game_Level = UserLevel;
        newAttempt.experience_Points_per_Attempt = ExperiencePoints;
        newAttempt.level_Completed = result; //poner en false si no completó el nivel

        _listAttempts.list.Add(newAttempt);
        _saveSystem.SetListAttempts(_listAttempts);
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