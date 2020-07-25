using UnityEngine;

public class UIGameHandler : MonoBehaviour
{
    //Variables seteables desde el inspector. OJO, se recomienda hacerlo por FindObjectsOfType para evitar problemas de NullReference.

    #region Declaracion de variables
    [Header("InGame Settings")]
    [SerializeField] GameObject _inGameScreen;
    [Header("Pause Settings")]
    [SerializeField] GameObject _pauseScreen;
    [Header("Level complete Settings")]
    [SerializeField] GameObject _levelCompleteScreen;

    Manager _manager;

    public bool IsLastScene { get; private set; }
    public bool Lose { get; private set; }


    #endregion

    #region Funciones de Unity

    private void Awake()
    {
        //Obtengo la referencia al Manager
        _manager = FindObjectOfType<Manager>();

        //Suscribo eventos al iniciar el nivel.
        SubscribeEvents();
    }

    private void Start()
    {
        //Cargo el estado de si esta es la última escena
        if (_manager != null)
            IsLastScene = _manager.IsLastScene;
    }
    private void OnDisable()
    {
        //Se ejecuta solamente al cambiar de escena.
        //Desuscribe eventos para que no queden en la próxima escena.
        UnsubscribeEvents();
    }
    #endregion

    #region Subscripcion y desuscripcion de eventos

    private void SubscribeEvents()
    {
        EventsHandler.SubscribeToEvent("GP_PAUSE", PauseScreen);
        EventsHandler.SubscribeToEvent("GP_RESUME", ResumeScreen);
        EventsHandler.SubscribeToEvent("GP_LEVELCOMPLETE", EndOfLevelScreen);
        EventsHandler.SubscribeToEvent("GP_LOSE", LoseScreen);
    }
    private void UnsubscribeEvents()
    {
        EventsHandler.UnsubscribeToEvent("GP_PAUSE", PauseScreen);
        EventsHandler.UnsubscribeToEvent("GP_RESUME", ResumeScreen);
        EventsHandler.UnsubscribeToEvent("GP_LEVELCOMPLETE", EndOfLevelScreen);
        EventsHandler.UnsubscribeToEvent("GP_LOSE", LoseScreen);
    }

    #endregion

    #region Métodos

    //Metodos que activan o desactivan paneles.
    private void PauseScreen()
    {
        if (_pauseScreen != null && _inGameScreen != null)
        {
            _pauseScreen.SetActive(true);
            _inGameScreen.SetActive(false);
        }
    }

    //Activa la pantalla de reanudar juego
    private void ResumeScreen()
    {
        if (_pauseScreen != null && _inGameScreen != null)
        {
            _inGameScreen.SetActive(true);
            _pauseScreen.SetActive(false);
        }
    }

    //Levanta la pantalla de fin de nivel y apaga las demas
    private void EndOfLevelScreen()
    {
        if (_levelCompleteScreen != null && _inGameScreen != null && _pauseScreen != null)
        {
            _levelCompleteScreen.SetActive(true);
            _inGameScreen.SetActive(false);
            _pauseScreen.SetActive(false);
        }
    }

    //Al perder, setea lose en true (para desactivar el botón de siguiente nivel) y activa EndOfLevelScreen 
    private void LoseScreen()
    {
        Lose = true;
        EndOfLevelScreen();
    }

    #endregion
}
