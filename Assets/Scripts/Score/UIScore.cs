using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Muestra los puntos en pantalla
/// </summary>
public class UIScore : MonoBehaviour
{
    ExperienceHandler _scoreHandler;
    Text _pointsText;

    float _lastScore = 0;

    private void Awake()
    {
        _scoreHandler = FindObjectOfType<ExperienceHandler>();
        _pointsText = GetComponent<Text>();
    }

    private void Update()
    {
        if(_scoreHandler != null)
        {
            float points = _scoreHandler.ExpPoints;

            if(_lastScore != points)
            {
                _lastScore = points;
                _pointsText.text = "Score: " + points.ToString();
            }
        }
    }
}