using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceDisplay : MonoBehaviour
{
    [Header("Experience")]
    [SerializeField] Text _txtExpEarned;
    [SerializeField] Slider _slExpBar;
    [SerializeField] float _maxValue;

    ScoreHandler _scoreHandler;

    private void OnEnable()
    {
        //Si no tiene acces al ScoreHandler lo busca
        if (_scoreHandler == null)
            _scoreHandler = FindObjectOfType<ScoreHandler>();

        //Calcula y muestra en pantalla los puntos ganados en la partida actual
        float expPoints = _scoreHandler.ExpPoints;
        float initialExpPoints = _scoreHandler.InitialExpPoints;
        string expTextToShow = "+{0}";
        _txtExpEarned.text = string.Format(expTextToShow, expPoints - initialExpPoints);

        //Actualiza la barra de experiencia.
        _slExpBar.value = expPoints / _maxValue;
    }
}
