using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class UserInfo : MonoBehaviour
{
    SaveSystem _saveSystem;
    User _user;

    private void Awake() 
    {
        _saveSystem = FindObjectOfType<SaveSystem>();
        _user = _saveSystem.GetJson();
        
        //print(_user);

        Text[] children = GetComponentsInChildren<Text>();
        children[0].text = "Nombre: " + _user.name;
        children[1].text = "Puntos: " + _user.experiencePoints.ToString();
        children[2].text = "Nivel: " + _user.currentLevel.ToString();
    }
}
