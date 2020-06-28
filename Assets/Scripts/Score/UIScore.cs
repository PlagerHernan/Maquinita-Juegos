using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    ScoreHandler _scoreHandler;
    Text _pointsText;

    float _lastScore = 0;

    private void Awake()
    {
        _scoreHandler = FindObjectOfType<ScoreHandler>();
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