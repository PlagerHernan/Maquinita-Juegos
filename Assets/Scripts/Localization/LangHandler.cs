using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

//Enums Idiomas
public enum Language
{
    English,
    Spanish,
    Portuguese
}

public class LangHandler : MonoBehaviour
{
    //Enum de idiomas.
    private Language selectedLanguage;
    //Creo la propiedad del idioma elegido. Cuando se cambia el idioma, se actualizan los textos.
    public Language SelectedLanguage
    {
        get => selectedLanguage;
        set
        {
            _manager.Language = selectedLanguage = value;
            OnUpdate();
        }
    }

    //Diccionario de Lenguaje, que va a contener otro diccionario que va a tomar como key un ID y como valor el texto correspondiente
    private Dictionary<string, Dictionary<Language, string>> languageManager;

    //Creo una variable estatica que guardará los textos de forma que persistan entre escenas.
    //Y no tener que descargarlas todo el tiempo de la nube.
    private static Dictionary<string, Dictionary<Language, string>> _savedCodex;

    //Url para saber desde donde descargar nuestro documento
    [SerializeField] string externalUrl;

    // un evento para actualizar cuando se tiene que cambiar el texto
    public event Action OnUpdate = delegate { };

    //Accedo al manager para cambiar la variable de idioma
    private Manager _manager;


    //Bajamos el Archivo de internet o lo levantamos desde el disco
    void Awake()
    {
        _manager = FindObjectOfType<Manager>();

        if (_manager != null)
            selectedLanguage = _manager.Language;

        if(_savedCodex == null)
        {
            //Para crear el ejecutable,tirar el archivo .csv dentro de la carpeta Nombre_Data   
            //LanguageManager = LanguageU.loadCodexFromString("NombreDelDoc.csv", File.ReadAllText(Application.dataPath + "/NombreDelDoc.csv"));

            if(externalUrl != "")
                StartCoroutine(DownloadCSV(externalUrl));
        }
        else
        {
            languageManager = _savedCodex;
        }
    }

    /// <summary>
    /// En base a una ID devolvemos la texto correspondiente
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public string GetTranslate(string _id)
    {
        if (languageManager == null || !languageManager[_id].ContainsKey(selectedLanguage))
            return "";
        else
            return languageManager[_id][selectedLanguage];
    }
    

    /// <summary>
    /// Bajamos el documento desde la pagina indicada por parametros y la cortamos 
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public IEnumerator DownloadCSV(string url)
    {
        url = System.Text.RegularExpressions.Regex.Replace(url, "edit.+", "export?format=csv");
        var www = new UnityWebRequest(url);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();    
        languageManager = LanguageExtractor.ExtractTexts("www", www.downloadHandler.text);
        _savedCodex = languageManager;

        OnUpdate();
    }

    /// <summary>
    /// Funcion para guardar el documento en disco y tener un backup
    /// </summary>
    //public void ReWriteText(byte[] newText)
    //{
    //    if (File.Exists(Application.dataPath + "/NombreDelDoc.csv"))
    //    {
    //        if (File.Exists(Application.dataPath + "/NombreDelDocBackup.csv"))
    //            File.Delete(Application.dataPath + "/NombreDelDocBackup.csv");
    //        File.Copy(Application.dataPath + "/NombreDelDoc.csv", Application.dataPath + "/NombreDelDocBackup.csv");
    //        File.Delete(Application.dataPath + "/NombreDelDoc.csv");
    //        File.WriteAllBytes(Application.dataPath + "/NombreDelDoc.csv", newText);
    //    }
    //    else
    //    {
    //        File.WriteAllBytes(Application.dataPath + "/NombreDelDoc.csv", newText);
    //    }

    //}

    //Cargo el idioma guardado
    private void Start()
    {
        selectedLanguage = _manager.Language;
    }
}