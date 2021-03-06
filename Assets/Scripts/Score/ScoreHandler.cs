﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    GameManager _gameManager;
    float _expPoints, _initialExpPoints = 0;

    public float ExpPoints { get => _expPoints; }
    public float InitialExpPoints { get => _initialExpPoints; }

    private void Awake()
    {
        _gameManager = GetComponent<GameManager>();
    }

    private void Start()
    {
        if(_gameManager != null)
        {
            _expPoints = _initialExpPoints = _gameManager.ExperiencePoints;
        }
    }

    public void OnClickAddPoint(int p)
    {
        //Expresion que clampea el valor entrante a un valor mayor a 0.
        p = p < 0 ? 0 : p;
        //Consta de tres partes,
        //  1- La primera es la condicion (p < 0) delimitada por el signo "?",
        //  2- Valor que se asigna si la condicion da true (el 0 que está del lado izquierdo del ":")
        //  3- Valor que se asigna si la condicion da false (la "p" que está del lado derecho del ":")

        _expPoints += p;
        _gameManager.ExperiencePoints = _expPoints;
    }


}
