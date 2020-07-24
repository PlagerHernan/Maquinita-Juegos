using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour
{
    [Tooltip("IDs de los idiomas a cargar al dropdown. Deben estar en el mismo orden que el definido por el enum Language")]
    [SerializeField] string[] _langTextIDs;

    //Array que contendrá los textos de idiomas
    string[] _langTexts;

    //Referencia al LangHandler para obtener y setear el idioma actual.
    LangHandler _langHandler;

    //Referencia al propio dropdown
    Dropdown _dropdown;

    //Declaro variable para guardar el idioma actual
    Language _selectedLanguage;

    //Declaro un booleano para prevenir stackoverflow al cambiar valores
    bool _alreadyExecuted = false;

    private void Awake()
    {
        _langHandler = FindObjectOfType<LangHandler>();
        _dropdown = GetComponent<Dropdown>();

        if(_langTextIDs != null)
        {
            _langTexts = new string[_langTextIDs.Length];
            _langHandler.OnUpdate += ChangeLang;
        }
    }

    private void OnEnable()
    {
        //Suscribo el método de cambio de idioma al dropdown,
        //para que este se ejecute cuando el dropdown cambia de valor
        _dropdown.onValueChanged.AddListener(delegate { OnValueChanged(_dropdown); });
    }
    private void OnDisable()
    {
        //Cuando el objeto se desactiva, es decir cuando cambio de pantalla,
        //remuevo el método del dropdown para que no quede almacenado.
        _dropdown.onValueChanged.RemoveListener(delegate { OnValueChanged(_dropdown); });
    }

    private void Start()
    {
        if(_langHandler != null)
        {
            //Guardo el idioma actual seleccionado
            _selectedLanguage = _langHandler.SelectedLanguage;

            //Guardo los idiomas del enum Language en forma de lista de strings
            var languages = new List<string>();
            var temp = (Language[])System.Enum.GetValues(typeof(Language));
            foreach (var lang in temp)
            {
                languages.Add(lang.ToString());
            }

            //Actualizo las opciones y asigno 
            _dropdown.ClearOptions();
            _dropdown.AddOptions(languages);
            _dropdown.value = (int)_selectedLanguage;
        }
    }

    //Método que cambia el idioma actual.
    //El mismo se ejecuta cuando el dropdown cambia de valor (ejemplo cuando paso de ingles a español).
    void OnValueChanged(Dropdown change)
    {
        _selectedLanguage = (Language)change.value;

        #region Warning: Recursion. Proceda con precaucion y no altere la medida de seguridad
        //Este if es una MEDIDA DE SEGURIDAD para evitar ejecución recursiva (y por ende un stackoverflow).
        //Si no lo tuviera el proceso que ocurriría es el siguiente:
        //1- Asigno el idioma elegido a la propiedad en el LangHandler, el cual a su vez ejecuta el delegado OnUpdate();
        //2- El metodo ChangeLang() de este script está suscrito al delegado, por ende se ejecuta.
        //3- Cuando le cambio el valor al dropdown, se ejecuta el OnValueChanged (este mismo método). Esto pasa porque cambia de valor.
        //4- Se vuelve a repetir el paso 1
        if (!_alreadyExecuted)
        {
            _alreadyExecuted = true;
            _langHandler.SelectedLanguage = _selectedLanguage;
            _alreadyExecuted = false;
        }
        #endregion
    }

    //suscribo un método al OnUpdate del LangHandler para que actualice mis textos
    void ChangeLang()
    {
        for (int i = 0; i < _langTextIDs.Length; i++)
        {
            _langTexts[i] = _langHandler.GetTranslate(_langTextIDs[i]);
        }

        var langList = _langTexts.ArrayToList();

        _dropdown.ClearOptions();
        _dropdown.AddOptions(langList);
        _dropdown.value = (int)_selectedLanguage;
    }
}
