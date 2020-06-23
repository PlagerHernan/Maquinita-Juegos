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
            SceneManager.LoadScene(targetScene);    

            //Cantidad total de escenas en el proyecto
            //int sceneCount = SceneManager.sceneCountInBuildSettings;
        }
        else
        {
            print("Nivel bloqueado");
        }
    }
}
