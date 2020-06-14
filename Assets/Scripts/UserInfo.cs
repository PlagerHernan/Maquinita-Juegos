using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using UnityEngine.UI;


public class UserInfo : MonoBehaviour
{
    SaveSystem _saveSystem;
    User _user;

    private void Awake() 
    {
        _saveSystem = new SaveSystem();
        _user = _saveSystem.GetJson();
        //GetComponent<Text>().text = _user.currentLevel.ToString();

        Text[] children = GetComponentsInChildren<Text>();
        children[0].text = "test: " + _user.name;
    }
}
