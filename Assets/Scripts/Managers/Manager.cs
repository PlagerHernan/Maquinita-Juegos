using UnityEngine;

public class Manager : MonoBehaviour
{
    //referencias
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

    //variables de Settings
    [Header("Audio Settings")]
    [SerializeField] protected bool _musicState = true; 
    [SerializeField] protected bool _soundState = true;

    //variables de User
    protected string _userName;
    protected int _userLevel;
    protected float _userExperiencePoints;

    //propiedades de Settings
    public bool MusicOn { get => _musicState; set => _musicState = value; }
    public bool SoundOn { get => _soundState; set => _soundState = value; }

    //propiedades de User
    public string UserName { get => _userName; set => _userName = value; }
    public int UserLevel { get => _userLevel; set => _userLevel = value; }
    public float UserExperiencePoints { get => _userExperiencePoints; set => _userExperiencePoints = value; }

    public float ExperiencePoints { get => _user.experiencePoints; set => _user.experiencePoints = value; }
    public bool IsLastScene { get => _isLastScene; }

    virtual protected void Awake()
    {
        _saveSystem = FindObjectOfType<SaveSystem>();

        _gameSettings = _saveSystem.GetGameSettings(); 
        LoadSettingsInfo();

        _user = _saveSystem.GetUser();
        LoadUserInfo();
    }

    void LoadSettingsInfo()
    {
        _musicState = _gameSettings.musicOn;
        _soundState = _gameSettings.soundFXOn;
    }
    
    void LoadUserInfo()
    {
        _userName = _user.name;
        _userLevel = _user.currentLevel;
        _userExperiencePoints = _user.experiencePoints;
    }

    public void SaveSettingsInfo()
    {
        _gameSettings.musicOn = _musicState;
        _gameSettings.soundFXOn = _soundState;
		_saveSystem.SetGameSettings(_gameSettings); 
    }

    public void SaveUserInfo()
    {
        _user.currentLevel = _userLevel;
        _user.experiencePoints = _userExperiencePoints;
        _saveSystem.SetUser(_user);
    }

    //cierre forzado
    void OnApplicationQuit() 
    {
        SaveSettingsInfo();
        SaveUserInfo();
    }
}