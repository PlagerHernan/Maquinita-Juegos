using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Globalization;

public class TestAttempt : MonoBehaviour
{
    SaveSystem _saveSystem;
    GameManager _gameManager;

    private void Awake() 
    {
        _saveSystem = FindObjectOfType<SaveSystem>(); 
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start() 
    {
        //PrintDateTimeNow();

        //PrintTimefromJson();
    }

    private void Update() 
    {
        //Simula envío de lista de partidas al backend
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            //luego del envío, se elimina json con la lista de partidas
            _saveSystem.DeleteListAttempts();
            print("Lista de partidas eliminada");
        }   

        //Simula un hit del usuario
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddHit();
            print("Hit agregado");
        }

        //Simula un error del usuario
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddError();
            print("Error agregado");
        }
    }

    void AddHit()
    {
        _gameManager.HitsAttempt++;
    }
    void AddError()
    {
        _gameManager.ErrorsAttempt++;
    }

    //al convertir de Json a DateTime, le agrega 3 horas más (posible problema con el formato, ver zona horaria) 
    void PrintDateTimeNow()
    {
        var time = DateTime.Now;
        print(time);
        string json = JsonUtility.ToJson((JsonDateTime) time);
        Debug.Log("Converted to json");
        print(json);
        DateTime timeFromJson = JsonUtility.FromJson<JsonDateTime>(json);
        //DateTime timeFromJson = Convert.ToDateTime(json); //alternativa (no parece funcionar)
        Debug.Log("Converted to time");
        print(timeFromJson);
    }

    //Convierte los datos de tiempo del Json para hacerlos más legibles 
    /* void PrintTimefromJson()
    {
        //Agregar inicio de partida (string)
        DateTime attemptStart = JsonUtility.FromJson<JsonDateTime>();
        print("Inicio de partida: " + attemptStart);

        //Agregar fin de partida (string)
        DateTime attemptEnd = JsonUtility.FromJson<JsonDateTime>();
        print("Fin de partida: " + attemptEnd);

        //Agregar tiempo de partida (int) 
        var timeSpan = TimeSpan.FromSeconds();
        print("Tiempo total de la partida: " + string.Format("{0:00}:{1:00}:{2:00}", timeSpan.TotalHours, timeSpan.TotalMinutes, timeSpan.Seconds));
    } */
}
