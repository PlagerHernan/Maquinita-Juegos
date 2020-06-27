using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitApp : MonoBehaviour
{
	SaveSystem _saveSystem;
    GameSettings _gameSettings;

	//llamado desde Exit Button 
    public void ExitApp()
	{
		Application.Quit();
		Debug.Log ("aplicación cerrada");
	}
}
