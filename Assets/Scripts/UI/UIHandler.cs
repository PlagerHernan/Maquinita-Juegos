using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    //Variables seteables desde el inspector. OJO, se recomienda hacerlo por FindObjectsOfType para evitar problemas de NullReference.

    #region Declaracion de variables
    [Header("InGame Settings")]
    [SerializeField] GameObject _inGameScreen;
    [Header("Pause Settings")]
    [SerializeField] GameObject _pauseScreen;
    [Header("Level complete Settings")]
    [SerializeField] GameObject _levelCompleteScreen;
    #endregion

    #region Funciones de Unity

    private void Awake()
    {
        //Obtengo los distintos paneles del canvas. La variable "true" de los GetComponents indican que tambien busque componentes inactivos.
        _inGameScreen = GetComponentInChildren<UIInGame>(true).gameObject;
        _pauseScreen = GetComponentInChildren<UIPause>(true).gameObject;
        _levelCompleteScreen = GetComponentInChildren<UILevelComplete>(true).gameObject;

        SubscribeEvents();
    }

    #endregion

    #region Metodos privados

    private void SubscribeEvents()
    {
        EventsManager.SubscribeToEvent("GP_PAUSE", PauseScreen);
        EventsManager.SubscribeToEvent("GP_RESUME", ResumeScreen);
        EventsManager.SubscribeToEvent("GP_LEVELCOMPLETE", LevelCompleteScreen);
    }

    #endregion

    #region Métodos en EventsManager
    
    //Metodos que activan o desactivan paneles.
    public void PauseScreen()
    {
        if (_pauseScreen != null && _inGameScreen != null)
        {
            _pauseScreen.SetActive(true);
            _inGameScreen.SetActive(false);
        }
    }

    public void ResumeScreen()
    {
        if (_pauseScreen != null && _inGameScreen != null)
        {
            _inGameScreen.SetActive(true);
            _pauseScreen.SetActive(false);
        }
    }

    public void LevelCompleteScreen()
    {
        if (_levelCompleteScreen != null && _inGameScreen != null && _pauseScreen != null)
        {
            _levelCompleteScreen.SetActive(true);
            _inGameScreen.SetActive(false);
            _pauseScreen.SetActive(false);
        }
    }

    #endregion
}
