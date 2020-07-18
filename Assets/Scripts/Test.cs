using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Globalization;

public class Test : MonoBehaviour
{
    SaveSystem _saveSystem;
    ListAttempts _listAttempts;
    Manager _manager;

    int _countAttempts; //simula ID de attempt

    private void Awake() 
    {
        _saveSystem = FindObjectOfType<SaveSystem>();    
        _manager = FindObjectOfType<Manager>();
    }

    private void Start() 
    {
        //_listAttempts = _saveSystem.GetListAttempts();

        //_manager.AddNewAttempt();
        //AddNewAttempt();

        //_listAttempts.PrintList();

        //_saveSystem.SetListAttempts(_listAttempts);

        //PrintDateTime();
    }

    private void Update() 
    {
        //botón delete: simula envío de lista de partidas al backend
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            //luego del envío, se elimina json con la lista de partidas
            _saveSystem.DeleteListAttempts();
            print("Lista de partidas eliminada");
        }    
    }

    /* void AddNewAttempt()
    {
        _listAttempts = _saveSystem.GetListAttempts();

        Attempt newAttempt = new Attempt();

        //newAttempt.ID_Attempt = _countAttempts;
        newAttempt.current_Game_Level = _manager.UserLevel;
        newAttempt.experience_Points_per_Attempt = _manager.ExperiencePoints;
        newAttempt.level_Completed = true;

        _listAttempts.list.Add(newAttempt);
    } */ 

    //funciona mal
    void PrintDateTime()
    {
        var time = DateTime.Now;
        print(time);
        string json = JsonUtility.ToJson((JsonDateTime) time);
        print(json);
        DateTime timeFromJson = JsonUtility.FromJson<JsonDateTime>(json);
        //DateTime timeFromJson = Convert.ToDateTime(json);
        print(timeFromJson);
    }
}
