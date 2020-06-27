using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour
{
    AudioSource _audioSource;

    /* private void Awake() 
    {
        _audioSource = GetComponent<AudioSource>();

        //empieza mostrando sólo el menú principal
        GameObject[] _menus = GameObject.FindGameObjectsWithTag("Menu");
        foreach (GameObject menu in _menus)
        {
            if (menu.name == "Main Menu")
            {
                menu.SetActive(true);
            }
            else
            {
                menu.SetActive(false);
            }
        } 
    } */
}
