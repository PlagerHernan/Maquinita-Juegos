using UnityEngine;

public class UIMenuHandler : MonoBehaviour
{
    #region Declaracion de variables

    [Header("Menu Screen Settings")]
    [SerializeField] GameObject _menuScreen;
    [Header("Settings Screen Settings")]
    [SerializeField] GameObject _settingsScreen;
    [Header("Level Selection Settings")]
    [SerializeField] GameObject _levelSelectScreen;
    [Header("Credits Settings")]
    [SerializeField] GameObject _creditsScreen;

    #endregion

    #region Funciones de Unity
    private void Awake()
    {
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
        EventsManager.SubscribeToEvent("GP_MAINMENU", MenuScreen);
        EventsManager.SubscribeToEvent("GP_SETTINGS", SettingsScreen);
        EventsManager.SubscribeToEvent("GP_CREDITS", CreditsScreen);
        EventsManager.SubscribeToEvent("GP_LEVELSELECT", LevelSelectScreen);
    }
    private void UnsubscribeEvents()
    {
        EventsManager.UnsubscribeToEvent("GP_MAINMENU", MenuScreen);
        EventsManager.UnsubscribeToEvent("GP_SETTINGS", SettingsScreen);
        EventsManager.UnsubscribeToEvent("GP_CREDITS", CreditsScreen);
        EventsManager.UnsubscribeToEvent("GP_LEVELSELECT", LevelSelectScreen);
    }

    #endregion

    #region Metodos

    //HARDCODED. Desactiva todos los paneles y activa el de Menu.
    private void MenuScreen()
    {
        if (_menuScreen != null && _settingsScreen != null && _levelSelectScreen != null)
        {
            _menuScreen.SetActive(true);
            _settingsScreen.SetActive(false);
            _levelSelectScreen.SetActive(false);
        }
    }

    //HARDCODED. Desactiva todos los paneles y activa el de Settings
    private void SettingsScreen()
    {
        if (_menuScreen != null && _settingsScreen != null && _levelSelectScreen != null && _creditsScreen != null)
        {
            _menuScreen.SetActive(false);
            _settingsScreen.SetActive(true);
            _levelSelectScreen.SetActive(false);
            _creditsScreen.SetActive(false);
        }
    }

    //HARDCODED. Activa la pantalla de creditos que está en la pantalla de ajustes.
    private void CreditsScreen()
    {
        if(_creditsScreen != null)
            _creditsScreen.SetActive(true);
    }

    //HARDCODED. Activa la pantalla de seleccion de niveles y desactiva las demas.
    private void LevelSelectScreen()
    {
        if (_menuScreen != null && _settingsScreen != null && _levelSelectScreen != null && _creditsScreen != null)
        {
            _menuScreen.SetActive(false);
            _settingsScreen.SetActive(false);
            _levelSelectScreen.SetActive(true);
            _creditsScreen.SetActive(false);
        }
    }

    #endregion
}