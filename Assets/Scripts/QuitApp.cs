using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitApp : MonoBehaviour
{
	MenuManager _menuManager;

	//llamado desde Exit Button 
    public void ExitApp()
	{
		_menuManager = GameObject.FindObjectOfType<MenuManager>();
		_menuManager.SaveSettingsInfo();

		Application.Quit();
		Debug.Log ("aplicación cerrada");
	}
}
