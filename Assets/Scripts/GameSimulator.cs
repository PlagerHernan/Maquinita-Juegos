using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSimulator : MonoBehaviour
{
    #region Variables
    
    SaveSystem _saveSystem;
    User _user;
    [SerializeField] int _level;
    Slider _slider;
    Toggle _toggle;
    GameObject _toggleCheck;

    #endregion

    private void Awake() 
    {
        #region Inicialización de variables 

        _saveSystem = FindObjectOfType<SaveSystem>();
        _user = _saveSystem.GetUser();

        _slider = GetComponentInChildren<Slider>();
        _toggle = GetComponentInChildren<Toggle>();
        _toggleCheck = _toggle.transform.GetChild(0).gameObject;

        #endregion

        _slider.value = _user.experiencePoints;

        //si ya completó el nivel, no aparece el check (sólo texto)
        if (_user.currentLevel > _level)
        {
            _toggleCheck.SetActive(false);
        }
        else
        {
            _toggleCheck.SetActive(true);
        }
    }


    //si se cierra la aplicación en medio del juego
    private void OnApplicationQuit() 
    {
        SaveInfo();
    }

    //llamado desde Back to Menu Button o OnApplicationQuit()
    public void SaveInfo()
    {
        _user.experiencePoints = _slider.value;

        //si no había completado el nivel y lo acaba de completar, suma un nivel
        if (_user.currentLevel <= _level && _toggle.isOn)
        {
            _user.currentLevel = _level + 1;
        }
        
        _saveSystem.SetUser(_user);
    }
}
