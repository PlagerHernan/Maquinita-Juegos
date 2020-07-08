using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class UserInfo : MonoBehaviour
{
    MenuManager _menuManager; 

    private void Awake() 
    {
        _menuManager = FindObjectOfType<MenuManager>();

        Text[] children = GetComponentsInChildren<Text>();
        children[0].text = "Nombre: " + _menuManager.UserName;
        children[1].text = "Puntos: " + _menuManager.UserExperiencePoints.ToString();
        children[2].text = "Nivel: " + _menuManager.UserLevel;
    }    
}
