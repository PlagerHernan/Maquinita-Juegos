public class ToggleSetterMusic : ToggleSetter
{
    protected void Start()
    {
        if(_toggle != null)
        {
            _toggle.isOn = _gameManager.MusicOff;
        }
    }
}