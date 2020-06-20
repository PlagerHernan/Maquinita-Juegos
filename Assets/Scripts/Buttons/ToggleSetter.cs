using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSetter : MonoBehaviour
{
    protected Toggle _toggle;
    protected GameManager _gameManager;

    protected void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _gameManager = FindObjectOfType<GameManager>();
    }
}
