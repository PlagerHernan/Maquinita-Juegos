using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    string filePath;
    string jsonString;
    User _user;

    private void Awake() 
    {
        filePath = Application.dataPath + "/User.json";
        GetJson();
        SetJson();
        GetJson();
    }

    private void GetJson()
    {
        jsonString = File.ReadAllText(filePath);
        _user = JsonUtility.FromJson<User>(jsonString);

        print(_user);
    }

    private void SetJson()
    {
        _user.level = 2;
        _user.experiencePoints = 8.5f;

        jsonString = JsonUtility.ToJson(_user);
        File.WriteAllText(filePath, jsonString);
    }
}

[System.Serializable]
public class User
{
    public string userName;
    public int level;
    public float experiencePoints;

    public override string ToString()
    {
        return " Nombre: " + userName + " - Nivel: " + level + "\n Puntos de experiencia: " + experiencePoints;
    }
}
