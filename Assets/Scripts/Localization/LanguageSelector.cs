using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour
{
    //Referencia al LangHandler para obtener y setear el idioma actual.
    LangHandler _langHandler;

    //Referencia al propio dropdown
    Dropdown _dropdown;

    //Declaro variable para guardar el idioma actual
    Language _selectedLanguage;

    private void Awake()
    {
        _langHandler = FindObjectOfType<LangHandler>();
        _dropdown = GetComponent<Dropdown>();
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
        _langHandler.SelectedLanguage = _selectedLanguage;
    }
}
