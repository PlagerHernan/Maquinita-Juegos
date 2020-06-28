using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] SaveSystem _saveSystem;
    [SerializeField] GameSettings _gameSettings;
    public SaveSystem SaveSystem { get => _saveSystem; set => _saveSystem = value; }
    public GameSettings GameSettings { get => _gameSettings; set => _gameSettings = value; }

    protected void Awake() 
    {
        _saveSystem = FindObjectOfType<SaveSystem>();
		_gameSettings = _saveSystem.GetGameSettings(); 
    }

    /* [SerializeField] bool _musicState = true;
    [SerializeField] bool _soundState = true;

    public bool MusicOff { get => !_musicState; set => _musicState = !value; }
    public bool SoundOff { get => !_soundState; set => _soundState = !value; } */
}
