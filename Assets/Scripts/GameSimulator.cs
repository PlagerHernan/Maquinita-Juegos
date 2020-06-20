using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSimulator : MonoBehaviour
{
    SaveSystem _saveSystem;
    User _user;

    private void Awake() 
    {
        _saveSystem = FindObjectOfType<SaveSystem>();
        _user = _saveSystem.GetJson();

        GetComponentInChildren<Slider>().value = _user.experiencePoints;
    }

    //llamado desde Back to Menu Button
    public void SaveInfo()
    {
        _user.experiencePoints = GetComponentInChildren<Slider>().value;
        _saveSystem.SetJson(_user);
    }
}
