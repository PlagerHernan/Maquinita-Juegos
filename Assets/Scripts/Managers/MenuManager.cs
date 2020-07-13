using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Manager
{
    override protected void Awake()
    {
        base.Awake();
        print("MenuManager.Awake()");
    }

    private void Start() 
    {
        _gameSettings = _saveSystem.GetGameSettings(); 
        _user = _saveSystem.GetUser();    
    }
}