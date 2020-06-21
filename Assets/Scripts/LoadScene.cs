using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    SaveSystem _saveSystem;
    User _user;

    private void Awake() 
    {
        _saveSystem = FindObjectOfType<SaveSystem>();
        _user = _saveSystem.GetJson();
    }    

    //llamado desde Level Button y Back to Menu Button 
    public void ChangeScene(int targetScene)
    {
        //si el nivel está desbloqueado 
        if (targetScene <= _user.currentLevel)
        {
            switch (targetScene)
            {
                case 0: SceneManager.LoadScene("Menu"); break; //siempre desbloqueado (empieza desde 01)
                case 1: SceneManager.LoadScene("Level_01"); break; //siempre desbloqueado (empieza desde 01)
                case 2: SceneManager.LoadScene("Level_02"); break;
                case 3: SceneManager.LoadScene("Level_03"); break;
                default: print("escena inexistente"); break;
            } 
        }
        else
        {
            print("Nivel bloqueado");
        }
    }
}
