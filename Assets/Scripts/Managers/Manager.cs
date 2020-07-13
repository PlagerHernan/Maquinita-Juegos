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

    protected bool _lastMusicState;
    protected bool _lastSoundState;

    protected bool _isLastScene;

    protected float _baseTime;
    protected float _gameTime;

    //variables de User
    /*protected string _userName;
    protected int _userLevel;
    protected float _userExperiencePoints;*/

    //propiedades de Settings
    public bool MusicOn { get => _musicState; set => _musicState = value; }
    public bool SoundOn { get => _soundState; set => _soundState = value; }

    //propiedades de User
    public string UserName { get => _user.name; set => _user.name = value; }
    public int UserLevel { get => _user.currentLevel; set => _user.currentLevel = value; }
    public float ExperiencePoints { get => _user.experiencePoints; set => _user.experiencePoints = value; }
    
    public bool IsLastScene { get => _isLastScene; }
    public int CurrentLevel { get => _user.currentLevel;}

    virtual protected void Awake()
    {
        _saveSystem = FindObjectOfType<SaveSystem>();
    }

    public void SaveSettingsInfo()
    {
        _gameSettings.musicOn = _musicState;
        _gameSettings.soundFXOn = _soundState;
		_saveSystem.SetGameSettings(_gameSettings); 
    }

    public void SaveUserInfo()
    {
        _saveSystem.SetUser(_user);
    }

    //cierre forzado
    void OnApplicationQuit() 
    {
        SaveSettingsInfo();
        //SaveUserInfo();
    }
}