using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    string _userFilePath;
    string _gameSettingsFilePath;

    string _userJsonString;
    string _gameSettingsJsonString;
    User _user;

    public User GetJson()
    {
        _userFilePath = Application.dataPath + "/User.json";
        _userJsonString = File.ReadAllText(_userFilePath);
        _user = JsonUtility.FromJson<User>(_userJsonString);
        
        return _user;
    }
    public GameSettings GetGameSettings()
    {
        _gameSettingsFilePath = Application.dataPath + "/GameSettings.json";

        if (!File.Exists(_gameSettingsFilePath))
            CreateAndInitialiseGameSettings(_gameSettingsFilePath, GetDefaultSettings());
        
        _gameSettingsJsonString = File.ReadAllText(_gameSettingsFilePath);
        var gameSettings = JsonUtility.FromJson<GameSettings>(_gameSettingsJsonString);

        return gameSettings;
    }

    public void SetJson(User user)
    {
        //_user.currentLevel = 2;
        //_user.experiencePoints = 8.5f;
        _user = user;
        _userJsonString = JsonUtility.ToJson(user);
        File.WriteAllText(_userFilePath, _userJsonString);
    }

    public void SetGameSettings(GameSettings gameSettings)
    {
        _gameSettingsJsonString = JsonUtility.ToJson(gameSettings);
        File.WriteAllText(_gameSettingsFilePath, _gameSettingsJsonString);
    }

    //=================== PRIVATE METHODS ========================
    private void CreateAndInitialiseGameSettings(string path, GameSettings initialGameSettings)
    {
        if (File.Exists(path)) return;
        File.Create(path).Dispose();

        _gameSettingsJsonString = JsonUtility.ToJson(initialGameSettings);

        File.WriteAllText(path, _gameSettingsJsonString);
    }
    private GameSettings GetDefaultSettings()
    {
        var gS = new GameSettings();
        gS.musicOn = true;
        gS.soundFXOn = true;

        return gS;
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

[System.Serializable]
public struct GameSettings
{
    public bool musicOn;
    public bool soundFXOn;
}