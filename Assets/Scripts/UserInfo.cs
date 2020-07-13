using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class UserInfo : MonoBehaviour
{
    Manager _manager; 
    Text[] _children;

    private void Awake() 
    {
        _manager = FindObjectOfType<Manager>();
        _children = GetComponentsInChildren<Text>();
    }    

    private void Start() 
    {
        _children[0].text = "Nombre: " + _manager.UserName;
        _children[1].text = "Puntos: " + _manager.ExperiencePoints.ToString();
        _children[2].text = "Nivel: " + _manager.UserLevel;    
    }
}
