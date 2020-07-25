using System;
using UnityEngine;
using UnityEngine.UI;

public class UILevelComplete : MonoBehaviour
{
    [SerializeField] Button _nextLevelButton;
    [SerializeField] Text _scoreText;

    GameManager _gameManager;
    UIGameHandler _UIGameHandler;

    #region Métodos de Unity

    private void OnEnable()
    {
        //Si no hay un gameManager guardado, lo busca
        if(_gameManager == null)
            _gameManager = FindObjectOfType<GameManager>();

        if (_UIGameHandler == null)
        {
            _UIGameHandler = FindObjectOfType<UIGameHandler>();
        }

        //Obtiene el tiempo del juego y le aplica el formato de mm:ss.
        var timeSpan = TimeSpan.FromSeconds((int)_gameManager.GameTime);
        _scoreText.text = string.Format("{0:00}:{1:00}", timeSpan.TotalMinutes, timeSpan.Seconds);

        //Si es la última escena, desactiva el botón de siguiente nivel. Si no, lo activa.
        if (_UIGameHandler.IsLastScene || _UIGameHandler.Lose)
            _nextLevelButton.gameObject.SetActive(false);
        else
            _nextLevelButton.gameObject.SetActive(true);
    }

    #endregion
}