using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    string filePath;
    string jsonString;
    User _user;

    public User GetJson()
    {
        filePath = Application.dataPath + "/User.json";
        jsonString = File.ReadAllText(filePath);
        _user = JsonUtility.FromJson<User>(jsonString);
        
        return _user;
    }

    private void SetJson()
    {
        _user.currentLevel = 2;
        _user.experiencePoints = 8.5f;

        jsonString = JsonUtility.ToJson(_user);
        File.WriteAllText(filePath, jsonString);
    }
}

[System.Serializable]
public class User
{
    public string name;
    public int currentLevel;
    public float experiencePoints;

    public override string ToString()
    {
        return " Nombre: " + name + " - Nivel: " + currentLevel + "\n Puntos de experiencia: " + experiencePoints;
    }
}
