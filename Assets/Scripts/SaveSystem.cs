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

    #region Json Getters

    //Metodo para obtener el JSON de usuario
    public User GetJson()
    {
        _userFilePath = Application.dataPath + "/User.json";
        _userJsonString = File.ReadAllText(_userFilePath);
        User _user = JsonUtility.FromJson<User>(_userJsonString);
        
        return _user;
    }

    //Metodo para escribir el JSON de configuraciones de juego
    public GameSettings GetGameSettings()
    {
        // Seteo el path del archivo
        _gameSettingsFilePath = Application.dataPath + "/GameSettings.json";

        //Chequeo si el archivo exite, si existe lo crea, sino sigue de largo
        if (!File.Exists(_gameSettingsFilePath))
        {
            //Creo el arhivo e invoco el método Dispose() para que me lo deje listo para modificar.
            File.Create(_gameSettingsFilePath).Dispose();

            //Como no puedo inicializar un JSON con variables vacias, le pido al método GetDefaultSettings() que me inicialice la clase/estructura con datos por default.
            _gameSettingsJsonString = JsonUtility.ToJson(GetDefaultSettings());

            //Escribo el archivo
            File.WriteAllText(_gameSettingsFilePath, _gameSettingsJsonString);
        }
        
        //Obtengo los datos del JSON y lo paso
        _gameSettingsJsonString = File.ReadAllText(_gameSettingsFilePath);
        var gameSettings = JsonUtility.FromJson<GameSettings>(_gameSettingsJsonString);

        return gameSettings;
    }
    #endregion
    
    #region Json Setters

    //Metodo para escribir el JSON de usuario
    public void SetJson(User user)
    {
        //_user.currentLevel = 2;
        //_user.experiencePoints = 8.5f;
        //_user = user;
        _userJsonString = JsonUtility.ToJson(user);
        File.WriteAllText(_userFilePath, _userJsonString);
    }

    //Metodo para escribir el JSON de configuraciones de juego
    public void SetGameSettings(GameSettings gameSettings)
    {
        _gameSettingsJsonString = JsonUtility.ToJson(gameSettings);
        File.WriteAllText(_gameSettingsFilePath, _gameSettingsJsonString);
    }
    #endregion

    //=================== PRIVATE METHODS ========================

    //Metodo que devuelve un GameSettings con las variables por default
    private GameSettings GetDefaultSettings()
    {
        var gS = new GameSettings();
        gS.musicOn = true;
        gS.soundFXOn = true;

        return gS;
    }
}

[System.Serializable]
public struct User
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