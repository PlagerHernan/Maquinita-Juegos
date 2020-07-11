using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Manager
{
    #region Declaracion de variables

    //--Header es un atributo que imprime un encabezado en el inspector (ver inspector)
    //--SerializeField hace que una variable privada sea visible desde el inspector. Esto es para evitar utilizar variables públicas.

    [Header("Menu Scene")]
    [SerializeField] int _menuSceneIndex;
    #endregion

    #region Propiedades
    //Propiedades, encapsula variables privadas.
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
    override protected void Awake()
    {
        base.Awake();

        //Suscribe los eventos al manejador de eventos
        SubscribeMethodsToEventsManager();

        //Obtiene los datos de usuario y del juego mismo
        _user = _saveSystem.GetUser();

        LoadUserData();
        LoadSettings();
    }

    //Metodo de Unity que se ejecuta una sola vez antes del primer Update
    private void Start()
    {
        //La expresion que se encuentra a la derecha de la asignación es una comparación.
        //Compara si la escena actual es la última. Si la es, da true, sino da false.
        _isLastScene = SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1;

        //Obtiene las configuraciones mismas de juego y setea las opciones de juego.

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

        RefreshGameTime();
    }

    //cierre forzado
    private void OnApplicationQuit()
    {
        SaveSettings();
    }
    #endregion

    #region Subscripcion de eventos
    //Este metodo subscribe los metodos al manejador de eventos.
    private void SubscribeMethodsToEventsManager()
    {
        EventsManager.SubscribeToEvent("GP_RESUME", SaveSettings);
        EventsManager.SubscribeToEvent("GP_LEVELCOMPLETE", UnlockNextLevel);
        EventsManager.SubscribeToEvent("GP_LEVELCOMPLETE", SaveUserData);
    }
    #endregion

    #region Metodos privados
    //=============== PRIVATE METHODS =======================
    
    //Actualiza el tiempo de juego
    private void RefreshGameTime() => _gameTime = Time.time - _baseTime;
    private void LoadUserData() => _user = _saveSystem.GetUser();

    #endregion

    #region Eventos OnClick
    //=============== ONCLICK EVENTS ========================
    //Metodos destinados a botones

    public void OnClickMainMenu()
    {
        EventsManager.TriggerEvent("GP_MAIN_MENU");
    }
    public void OnClickPause()
    {
        EventsManager.TriggerEvent("GP_PAUSE");
    }
    public void OnClickResume()
    {
        EventsManager.TriggerEvent("GP_RESUME");
    }
    public void OnClickRestart()
    {
        EventsManager.TriggerEvent("GP_RESTART");
    }
    public void OnClickNextLevel()
    {
        EventsManager.TriggerEvent("GP_NEXT_LEVEL");
    }
    #endregion

    #region Métodos en EventsManager
    //========= EVENTS HANDLED BY EVENTSMANAGER ============
    //Metodos que se cargaran al manejador de eventos.

    //Carga los settings de juego del Json
    public void LoadSettings()
    {
        _gameSettings = _saveSystem.GetGameSettings();
        _musicState = _gameSettings.musicOn;
        _soundState = _gameSettings.soundFXOn;
    }

    //Guarda los settings de juego al Json
    public void SaveSettings()
    {
        _gameSettings.musicOn = _musicState;
        _gameSettings.soundFXOn = _soundState;

        var oldGameSettings = _saveSystem.GetGameSettings();

        if(!oldGameSettings.Equals(_gameSettings))
            _saveSystem?.SetGameSettings(_gameSettings);
    }

    //Guarda información de usuario en el JSON
    public void SaveUserData()
    {
        var oldUserData = _saveSystem.GetUser();

        if (!oldUserData.Equals(_user))
            _saveSystem?.SetUser(_user);
    }

    public void UnlockNextLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (!IsLastScene && _user.currentLevel <= sceneIndex)
            _user.currentLevel = sceneIndex + 1;
    }
    #endregion
}