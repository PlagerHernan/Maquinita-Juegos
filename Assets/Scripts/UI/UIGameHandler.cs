using System.Collections;
using System.Collections.Generic;
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
    bool _isLastScene; public bool IsLastScene { get => _isLastScene; }
    bool _lose = false; public bool Lose { get => _lose; }


    #endregion

    #region Funciones de Unity

    private void Awake()
    {
        _manager = FindObjectOfType<Manager>();
        //Suscribo eventos al iniciar el nivel.
        SubscribeEvents();
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
        EventsManager.SubscribeToEvent("GP_PAUSE", PauseScreen);
        EventsManager.SubscribeToEvent("GP_RESUME", ResumeScreen);
        EventsManager.SubscribeToEvent("GP_LEVELCOMPLETE", EndOfLevelScreen);
        EventsManager.SubscribeToEvent("GP_LOSE", LoseScreen);
    }
    private void UnsubscribeEvents()
    {
        EventsManager.UnsubscribeToEvent("GP_PAUSE", PauseScreen);
        EventsManager.UnsubscribeToEvent("GP_RESUME", ResumeScreen);
        EventsManager.UnsubscribeToEvent("GP_LEVELCOMPLETE", EndOfLevelScreen);
        EventsManager.UnsubscribeToEvent("GP_LOSE", LoseScreen);
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

    private void ResumeScreen()
    {
        if (_pauseScreen != null && _inGameScreen != null)
        {
            _inGameScreen.SetActive(true);
            _pauseScreen.SetActive(false);
        }
    }

    public void EndOfLevelScreen()
    {
        if (_levelCompleteScreen != null && _inGameScreen != null && _pauseScreen != null)
        {
            _levelCompleteScreen.SetActive(true);
            _inGameScreen.SetActive(false);
            _pauseScreen.SetActive(false);
        }
    }

    //Al perder, setea lose en true (para desactivar el botón de siguiente nivel) y activa EndOfLevelScreen 
    public void LoseScreen()
    {
        _lose = true;
        EndOfLevelScreen();
    }

    #endregion
}
