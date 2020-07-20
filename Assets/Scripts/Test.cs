using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Globalization;

public class Test : MonoBehaviour
{
    SaveSystem _saveSystem;

    private void Awake() 
    {
        _saveSystem = FindObjectOfType<SaveSystem>(); 
    }

    private void Start() 
    {
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

    //al convertir de Json a DateTime, le agrega 3 horas más (posible problema con el formato, ver zona horaria) 
    void PrintDateTime()
    {
        var time = DateTime.Now;
        print(time);
        string json = JsonUtility.ToJson((JsonDateTime) time);
        print(json);
        DateTime timeFromJson = JsonUtility.FromJson<JsonDateTime>(json);
        //DateTime timeFromJson = Convert.ToDateTime(json); //alternativa (no parece funcionar)
        print(timeFromJson);
    }
}
