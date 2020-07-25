using UnityEngine;

public class SFXHandler : MonoBehaviour
{
    //Se pasa por inspector un audioclip con sonido click.
    [SerializeField] AudioClip _clickAudioClip;

    Manager _manager;
    AudioLoader _audioLoader;
    AudioSource _audioSource;

    private void Awake() 
    {
        _manager = FindObjectOfType<Manager>();
        _audioLoader = FindObjectOfType<AudioLoader>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start() 
    {
        //Carga el estado del sonido desde el manager. Si está desactivado, mutea el AudioSource, sino lo desmutea.
        _audioSource.mute = !_manager.SoundOn;
    }

    //Metodo que hace el sonido de Click
    public void ClickSound()
    {
        if(_clickAudioClip != null)
            PlaySound(_clickAudioClip.name);
    }

    //Reproduzco un sonido
    public void PlaySound(string name)
    {
        var clip = _audioLoader.GetSound(name);
        _audioSource.PlayOneShot(clip);
    }

    //Muteo el AudioSource
    public void Mute(bool state)
    {
        //Si mutear es falso, setea la propiedad en el manager a true (porque el sonido está en On) y desmutea el AudioSource
        _manager.SoundOn = !state;
        _audioSource.mute = state;
    }
}
