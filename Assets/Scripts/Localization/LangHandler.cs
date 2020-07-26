using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

//Enums Idiomas
public enum Language
{
    English,
    Spanish,
    Portuguese
}

enum TextEncoding
{
    ASCII,
    Unicode,
    UTF7,
    UTF8,
    UTF32
}

public class LangHandler : MonoBehaviour
{
    //Url para saber desde donde descargar nuestro documento
    [Header("Google Spreasheet Link")]
    [SerializeField] string _externalUrl;
    [Header("CSV file name (with the .csv extension)")]
    [SerializeField] string _localCSVFileName;
    //Tipo de encoder que se utilizará para crear el archivo local
    [Header("CSV Encoding")]
    [SerializeField] TextEncoding _textEncoding = TextEncoding.UTF8;

    //Enum de idiomas.
    private Language _selectedLanguage;
    //Creo la propiedad del idioma elegido. Cuando se cambia el idioma, se actualizan los textos.
    public Language SelectedLanguage
    {
        get => _selectedLanguage;
        set
        {
            _manager.Language = _selectedLanguage = value;
            OnUpdate();
        }
    }

    //Diccionario de Lenguaje, que va a contener otro diccionario que va a tomar como key un ID y como valor el texto correspondiente
    private Dictionary<string, Dictionary<Language, string>> _languageManager;

    //Creo una variable estatica que guardará los textos de forma que persistan entre escenas.
    //Y no tener que descargarlas todo el tiempo de la nube.
    private static Dictionary<string, Dictionary<Language, string>> _savedCodex;

    // un evento para actualizar cuando se tiene que cambiar el texto
    public event Action OnUpdate = delegate { };

    //Accedo al manager para cambiar la variable de idioma
    private Manager _manager;


    //Bajamos el Archivo de internet o lo levantamos desde el disco
    void Awake()
    {
        _manager = FindObjectOfType<Manager>();
        if (_manager != null)
        {
            _selectedLanguage = _manager.Language;
        }

        if(_savedCodex == null)//Si no tengo textos cargados en memoria
        {
            var docFormat = string.Format("/{0}", _localCSVFileName);

            //Intento acceder al CSV local
            if(_localCSVFileName != null && File.Exists(Application.dataPath + docFormat))
            {
                _languageManager = LanguageExtractor.ExtractTexts(_localCSVFileName, File.ReadAllText(Application.dataPath + docFormat));
                _savedCodex = _languageManager;
            }
            //Sino intento descargandolo de internet
            else if (_externalUrl != "")
            {
                Debug.Log("Descargo textos");
                StartCoroutine(DownloadCSV(_externalUrl));
            }
        }
        else
        {
            _languageManager = _savedCodex;
        }
    }

    /// <summary>
    /// En base a una ID devolvemos la texto correspondiente
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public string GetTranslate(string _id)
    {
        if (_languageManager == null || !_languageManager[_id].ContainsKey(_selectedLanguage))
            return "";
        else
            return _languageManager[_id][_selectedLanguage];
    }
    

    /// <summary>
    /// Bajamos el documento desde la pagina indicada por parametros y la cortamos 
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public IEnumerator DownloadCSV(string url)
    {
        //Convierto la url en uno compatible para importar textos como CSV
        url = System.Text.RegularExpressions.Regex.Replace(url, "edit.+", "export?format=csv");

        //Hago una request a google para descargar los textos.
        var www = new UnityWebRequest(url);
        www.downloadHandler = new DownloadHandlerBuffer();
        //Espero a que se descargue todo.
        yield return www.SendWebRequest();

        //Extraigo los textos y los almaceno en el diccionario.
        _languageManager = LanguageExtractor.ExtractTexts("www", www.downloadHandler.text);

        //Guardo el archivo de forma local.
        if(_localCSVFileName != "")
        {
            var data = www.downloadHandler.text;
            ReWriteText(data);
        }

        _savedCodex = _languageManager;

        OnUpdate();
    }

    /// <summary>
    /// Funcion para guardar el documento en disco y tener un backup
    /// </summary>
    public void ReWriteText(string newText)
    {
        var docFormat = string.Format("/{0}", _localCSVFileName);
        var docExtension = _localCSVFileName.Split('.')[1];
        var docFormatBackup = string.Format("/{0}_backup{1}", _localCSVFileName, docExtension);

        var encoding = GetEncoding(_textEncoding);
        var newTextInBytes = encoding.GetBytes(newText);

        if (File.Exists(Application.dataPath + docFormat))
        {
            if (File.Exists(Application.dataPath + docFormatBackup))
                File.Delete(Application.dataPath + docFormatBackup);
            File.Copy(Application.dataPath + docFormat, Application.dataPath + docFormatBackup);
            File.Delete(Application.dataPath + docFormat);
            File.WriteAllBytes(Application.dataPath + docFormat, newTextInBytes);
        }
        else
        {
            File.WriteAllBytes(Application.dataPath + docFormat, newTextInBytes);
        }

    }

    //Cargo el idioma guardado
    private void Start()
    {
        _selectedLanguage = _manager.Language;
    }

    Encoding GetEncoding(TextEncoding desiredEnconding)
    {
        Encoding encoding;

        switch (desiredEnconding)
        {
            case TextEncoding.ASCII:
                encoding = Encoding.ASCII;
                break;
            case TextEncoding.Unicode:
                encoding = Encoding.Unicode;
                break;
            case TextEncoding.UTF7:
                encoding = Encoding.UTF7;
                break;
            case TextEncoding.UTF8:
                encoding = Encoding.UTF8;
                break;
            case TextEncoding.UTF32:
                encoding = Encoding.UTF32;
                break;
            default:
                encoding = Encoding.UTF8;
                break;
        }

        return encoding;
    }
}