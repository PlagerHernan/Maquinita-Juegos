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
        if (_scoreHandler == null)
            _scoreHandler = FindObjectOfType<ScoreHandler>();

        float expPoints = _scoreHandler.ExpPoints;
        float initialExpPoints = _scoreHandler.InitialExpPoints;
        string expTextToShow = "+{0}";
        _txtExpEarned.text = string.Format(expTextToShow, expPoints - initialExpPoints);

        _slExpBar.value = expPoints / _maxValue;
    }
}
