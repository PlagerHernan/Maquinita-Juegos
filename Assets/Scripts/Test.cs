using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Globalization;

public class Test : MonoBehaviour
{
    SaveSystem _saveSystem;
    Attempt _attempt;
    ListAttempts _listAttempts;

    private void Awake() 
    {
        _saveSystem = FindObjectOfType<SaveSystem>();    
    }

    private void Start() 
    {
        //Attempt
        /*_attempt = _saveSystem.GetAttempt();

        _attempt.count ++;
        _attempt.text = "Hola mundo " + _attempt.count.ToString();
        
        _saveSystem.SetAttempt(_attempt);*/

        //ListAttempts
        _listAttempts = _saveSystem.GetListAttempts();

        AddNewAttempt();
        AddNewAttempt();
        _listAttempts.PrintList();

        _saveSystem.SetListAttempts(_listAttempts);

        /* DateTime dateTime = DateTime.Now;
        print(dateTime); */
    }

    void AddNewAttempt()
    {
        Attempt newAttempt = new Attempt();

        newAttempt.attempt_Starting_Point = DateTime.Now;
        newAttempt.attempt_End = DateTime.Now;
        
        _listAttempts.list.Add(newAttempt);
    }

    /* void FindInListAttempts()
    {
        Attempt foundAttempt = _listAttempts.list.Find(attempt => attempt.ID_Attempt == 0);
        print("foundAttempt: " + foundAttempt);
    } */

    //funcion para eliminar lista de partidas despues de mandarlo
}
