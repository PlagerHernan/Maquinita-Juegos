using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSimulator : MonoBehaviour
{
    #region Variables
    
    Manager _manager;

    [SerializeField] int _level;
    Slider _slider;
    Toggle _toggle;
    GameObject _toggleCheck;

    #endregion

    private void Awake() 
    {
        _manager = FindObjectOfType<Manager>();

        _slider = GetComponentInChildren<Slider>();
        _toggle = GetComponentInChildren<Toggle>();
        _toggleCheck = _toggle.transform.GetChild(0).gameObject;
    }

    private void Start() 
    {
        _slider.value = _manager.ExperiencePoints;

        //si ya completó el nivel, no aparece el check (sólo texto)
        if (_manager.UserLevel > _level)
        {
            _toggleCheck.SetActive(false);
        }
        else
        {
            _toggleCheck.SetActive(true);
        }
    }

    //llamado desde Back to Menu Button 
    public void SaveInfo()
    {
        _manager.ExperiencePoints = _slider.value;
        print("_slider.value: " + _slider.value);
        print("_manager.ExperiencePoints: " + _manager.ExperiencePoints);

        //si no había completado el nivel y lo acaba de completar, suma un nivel
        if (_manager.UserLevel <= _level && _toggle.isOn)
        {
            _manager.UserLevel = _level + 1;
        }
    }
}
