using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApp : MonoBehaviour
{
	//llamado desde Exit Button 
    public void ExitApp()
	{
		Application.Quit();
		Debug.Log ("aplicación cerrada");
	}
}
