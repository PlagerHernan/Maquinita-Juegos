public class ToggleSetterSound : ToggleSetter
{
    protected void Start()
    {
        if(_toggle != null)
        {
            _toggle.isOn = !(_gameManager.SoundOn);
        }
    }
}