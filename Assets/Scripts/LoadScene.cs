using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //llamado desde Level Button y Back to Menu Button 
    public void ChangeScene(string targetLevel)
    {
        SceneManager.LoadScene(targetLevel);
    }
}
