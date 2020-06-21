using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSimulator : MonoBehaviour
{
    SaveSystem _saveSystem;
    User _user;
    [SerializeField] int _level;
    Slider _slider;
    Toggle _toggle;
    GameObject _toggleCheck;

    private void Awake() 
    {
        _slider = GetComponentInChildren<Slider>();
        _toggle = GetComponentInChildren<Toggle>();
        _toggleCheck = _toggle.transform.GetChild(0).gameObject;

        _saveSystem = FindObjectOfType<SaveSystem>();
        _user = _saveSystem.GetJson();

        _slider.value = _user.experiencePoints;

        //si ya completó el nivel, la casilla estará marcada. Si no, estará desmarcada 
        if (_user.currentLevel > _level)
        {
            //_toggle.isOn = true;
            _toggleCheck.SetActive(false);
        }
        else
        {
            _toggle.isOn = false;
            _toggleCheck.SetActive(true);
        }
    }

    //llamado desde Back to Menu Button
    public void SaveInfo()
    {
        _user.experiencePoints = _slider.value;

        //
        if (_user.currentLevel <= _level && _toggle.isOn)
        {
            _user.currentLevel = _level + 1;
        }
        _saveSystem.SetJson(_user);
    }
}
