using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Declaracion de variables

    //Variables seteables desdeel inspector. OJO, se recomienda hacerlo por FindObjectsOfType para evitar problemas de NullReference.

    //--Header es un atributo que imprime un encabezado en el inspector (ver inspector)
    //--SerializeField hace que una variable privada sea visible desde el inspector. Esto es para evitar utilizar variables públicas.
    [Header("InGame Settings")]
    [SerializeField] GameObject _inGame;
    [Header("Pause Settings")]
    [SerializeField] GameObject _pauseScreen;
    [Header("Menu Scene")]
    [SerializeField] int _menuSceneIndex;
    [Header("Sound Settings")]
    [SerializeField] bool _musicState = true;
    [SerializeField] bool _soundState = true;

    //Variables privadas, no son visibles desde el inspector
    bool _lastMusicState;
    bool _lastSoundState;
    SoundManager _soundManager;
    SaveSystem _saveSystem;
    GameSettings _gameSettings;

    //Propiedades, encapsula variables privadas. Estas propiedades en específico son accedidas por los botones de música y sonido.
    //Estan negadas porque cuando el boton esta tachado, setea la variable a true.
    public bool MusicOff { get => !_musicState; set => _musicState = !value; }
    public bool SoundOff { get => !_soundState; set => _soundState = !value; }

    //bool _returnToMenu = false;
    //public bool ReturnToMenu { get ; set; }
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
    }

    //Metodo de Unity que se ejecuta una sola vez antes del primer Update
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
    #endregion

    //=============== PRIVATE METHDOS =======================
    //Este metodo subscribe los metodos al manejador de eventos.
    private void SubscribeMethodsToEventsManager()
    {
        EventsManager.SubscribeToEvent("GP_MAIN_MENU", LoadMenuScene);
        EventsManager.SubscribeToEvent("GP_PAUSE", Pause);
        EventsManager.SubscribeToEvent("GP_RESUME", Resume);
        EventsManager.SubscribeToEvent("GP_RESUME", SaveSettings);
    }

    //Setea el estado de la música, tanto el volumen como las variables del mismo script
    private void SetMusicState()
    {
        Debug.Log(_musicState);
        if (_soundManager == null) return;

        if (_musicState) _soundManager.SetMusicVolume(1);
        else _soundManager.SetMusicVolume(0);

        _gameSettings.musicOn = _musicState;

        Debug.Log(_musicState + " | " + _lastMusicState);
    }
    //Setea el estado del sonido, tanto el volumen como las variables del mismo script
    private void SetSFXState()
    {
        if (_soundManager == null) return;

        if (_soundState) _soundManager.SetSFXVolume(1);
        else _soundManager.SetSFXVolume(0);

        _gameSettings.soundFXOn = _soundState;
    }

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

    public void ClickSound() => SFXPlayer.instance.PlaySound("s_click");

    //========= EVENTS HANDLED BY EVENTSMANAGER ============

    //Metodos que se cargaran al manejador de eventos.
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
