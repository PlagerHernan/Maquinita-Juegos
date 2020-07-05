using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script que accede a un Toggle del mismo gameObject y setea su valor.
/// </summary>
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
