using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Manager
{
    #region Declaracion de variables

    //--Header es un atributo que imprime un encabezado en el inspector (ver inspector)
    //--SerializeField hace que una variable privada sea visible desde el inspector. Esto es para evitar utilizar variables públicas.
    
    [Header("Menu Scene")]
    [SerializeField] int _menuSceneIndex;
    [Header("Sound Settings")]
    [SerializeField] bool _musicState = true;
    [SerializeField] bool _soundState = true;

    //Variables privadas, no son visibles desde el inspector
    private bool _lastMusicState;
    private bool _lastSoundState;

    private bool _isLastScene;

    private float _baseTime;
    private float _gameTime;

    SoundManager _soundManager;
    SaveSystem _saveSystem;
    GameSettings _gameSettings;
    User _user;
    #endregion

    #region Propiedades
    //Propiedades, encapsula variables privadas.

    //Estas propiedades en específico son accedidas por los botones de música y sonido.
    //Estan negadas porque cuando el boton esta tachado, setea la variable a true.
    public bool MusicOff { get => !_musicState; set => _musicState = !value; }
    public bool SoundOff { get => !_soundState; set => _soundState = !value; }
    public float ExperiencePoints { get => _user.experiencePoints; set => _user.experiencePoints = value; }
    public bool IsLastScene { get => _isLastScene; }
    public float GameTime { get => _gameTime; }

    #endregion

    #region Metodos de Unity

    //Metodo propio de Unity que se ejecuta en modo editor cada vez que se hacen cambios en las variables del inspector.
    //En este caso evita que el index de escena que queremos que ejecute no sea menor a 0 ni mayor que la cantidad de escenas cargadas en el buildIndex
    private void OnValidate()
    {
        _menuSceneIndex = Mathf.Clamp(_menuSceneIndex, 0, SceneManager.sceneCount);
    }

    //Metodo de Unity que se ejecuta MIENTRAS SE INICIALIZAN LOS COMPONENTES.
    //Se ejecuta antes del Start y se utiliza mucho para establecer las referencias a otros scripts (FindObjectOfType, GetComponent, etc)
    private void Awake()
    {
        _soundManager = FindObjectOfType<SoundManager>();
        _saveSystem = FindObjectOfType<SaveSystem>();

        SubscribeMethodsToEventsManager();

        //Obtiene los datos de usuario
        _user = _saveSystem.GetUser();
    }

    //Metodo de Unity que se ejecuta una sola vez antes del primer Update
    private void Start()
    {
        //La expresion que se encuentra a la derecha de la asignación es una comparación.
        //Compara si la escena actual es la última. Si la es, da true, sino da false.
        _isLastScene = SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1;

        //Obtiene las configuraciones mismas de juego y setea las opciones de juego.
        LoadSettings();

        _lastMusicState = _musicState;
        _lastSoundState = _soundState;

        _baseTime = Time.time;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventsManager.TriggerEvent("GP_PAUSE");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventsManager.TriggerEvent("GP_LEVELCOMPLETE");
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

        RefreshGameTime();
    }
    #endregion

    #region Subscripcion de eventos
    //Este metodo subscribe los metodos al manejador de eventos.
    private void SubscribeMethodsToEventsManager()
    {
        EventsManager.SubscribeToEvent("GP_MAIN_MENU", LoadMenuScene);
        EventsManager.SubscribeToEvent("GP_RESUME", SaveSettings);
        EventsManager.SubscribeToEvent("GP_RESTART", Restart);
        EventsManager.SubscribeToEvent("GP_NEXT_LEVEL", LoadNextScene);
        EventsManager.SubscribeToEvent("GP_LEVELCOMPLETE", SaveUserData);
    }
    #endregion

    #region Metodos privados
    //=============== PRIVATE METHODS =======================

    //Setea el estado de la música, tanto el volumen como las variables del mismo script
    private void SetMusicState()
    {
        if (_soundManager == null) return;

        if (_musicState) _soundManager.SetMusicVolume(1);
        else _soundManager.SetMusicVolume(0);

        _gameSettings.musicOn = _musicState;
    }

    //Setea el estado del sonido, tanto el volumen como las variables del mismo script
    private void SetSFXState()
    {
        if (_soundManager == null) return;

        if (_soundState) _soundManager.SetSFXVolume(1);
        else _soundManager.SetSFXVolume(0);

        _gameSettings.soundFXOn = _soundState;
    }

    private void RefreshGameTime() => _gameTime = Time.time - _baseTime;

    #endregion

    #region Eventos OnClick
    //=============== ONCLICK EVENTS ========================
    //Metodos destinados a botones

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
    public void OnClickRestart()
    {
        ClickSound();
        EventsManager.TriggerEvent("GP_RESTART");
    }
    public void OnClickNextLevel()
    {
        ClickSound();
        EventsManager.TriggerEvent("GP_NEXT_LEVEL");
    }

    public void ClickSound() => SFXPlayer.instance.PlaySound("s_click");
    #endregion

    #region Métodos en EventsManager
    //========= EVENTS HANDLED BY EVENTSMANAGER ============
    //Metodos que se cargaran al manejador de eventos.
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(_menuSceneIndex);
    }
    public void LoadNextScene()
    {
        if (!_isLastScene)
        {
            int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("Este es el <color = red>último</color> nivel");
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            _saveSystem?.SetGameSettings(_gameSettings);
    }
    public void SaveUserData()
    {
        var oldUserData = _saveSystem.GetUser();

        if (!oldUserData.Equals(_user))
            _saveSystem?.SetUser(_user);
    }
    #endregion
}