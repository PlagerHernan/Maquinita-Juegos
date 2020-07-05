using System;
using UnityEngine;
using UnityEngine.UI;

public class UILevelComplete : MonoBehaviour
{
    [SerializeField] Button _nextLevelButton;
    [SerializeField] Text _scoreText;

    GameManager _gameManager;

    #region Métodos de Unity

    private void OnEnable()
    {
        //Si no hay un gameManager guardado, lo busca
        if(_gameManager == null)
            _gameManager = FindObjectOfType<GameManager>();

        //Obtiene el tiempo del juego y le aplica el formato de mm:ss.
        var timeSpan = TimeSpan.FromSeconds((int)_gameManager.GameTime);
        _scoreText.text = string.Format("{0:00}:{1:00}", timeSpan.TotalMinutes, timeSpan.Seconds);

        //Si es la última escena, desactiva el botón de siguiente nivel. Sino, lo activa.
        if (_gameManager.IsLastScene)
            _nextLevelButton.gameObject.SetActive(false);
        else
            _nextLevelButton.gameObject.SetActive(true);
    }

    #endregion
}