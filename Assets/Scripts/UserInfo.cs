using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class UserInfo : MonoBehaviour
{
    Manager _manager;
    LangHandler _langHandler;
    Text[] _children;

    string _username;
    string _expPoints;
    string _level;

    private void Awake() 
    {
        _manager = FindObjectOfType<Manager>();
        _children = GetComponentsInChildren<Text>();
        _langHandler = FindObjectOfType<LangHandler>();

        _langHandler.OnUpdate += ChangeLang;
    }    

    private void Start() 
    {
        ChangeLang();
    }

    void ChangeLang()
    {
        _username = _langHandler.GetTranslate("menu_menu_username");
        _expPoints = _langHandler.GetTranslate("menu_menu_exppoints");
        _level = _langHandler.GetTranslate("menu_menu_currlevel");

        _children[0].text = _username + _manager.UserName;
        _children[1].text = _expPoints + _manager.ExperiencePoints.ToString();
        _children[2].text = _level + _manager.UserLevel;
    }
}
