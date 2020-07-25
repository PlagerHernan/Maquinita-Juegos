using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Manager
{
    //--Header es un atributo que imprime un encabezado en el inspector (ver inspector)
    //--SerializeField hace que una variable privada sea visible desde el inspector. Esto es para evitar utilizar variables públicas.

    [Header("Current Scene")]
    [SerializeField] int _currentSceneIndex;

    private float _baseTime;
    private float _gameTime; public float GameTime { get => _gameTime; }

    //variables y propiedades de Attempt
    float _expPointsAttempt; public float ExpPointsAttempt { get => _expPointsAttempt; set => _expPointsAttempt = value; }
    private int _experienceLevel; public int ExperienceLevel { get => _experienceLevel; set => _experienceLevel = value; }
    int _hitsAttempt; public int HitsAttempt { get => _hitsAttempt; set => _hitsAttempt = value; }
    int _errorsAttempt; public int ErrorsAttempt { get => _errorsAttempt; set => _errorsAttempt = value; }
    string _attempt_Starting_Point; public string Attempt_Starting_Point { get => _attempt_Starting_Point; set => _attempt_Starting_Point = value; }

    #region Metodos de Unity

    //Metodo propio de Unity que se ejecuta en modo editor cada vez que se hacen cambios en las variables del inspector.
    //En este caso evita que el index de escena que queremos que ejecute no sea menor a 0 ni mayor que la cantidad de escenas cargadas en el buildIndex
    private void OnValidate()
    {
        _currentSceneIndex = Mathf.Clamp(_currentSceneIndex, 0, SceneManager.sceneCountInBuildSettings-1);
    }

    //Metodo de Unity que se ejecuta MIENTRAS SE INICIALIZAN LOS COMPONENTES.
    //Se ejecuta antes del Start y se utiliza mucho para establecer las referencias a otros scripts (FindObjectOfType, GetComponent, etc)
    override protected void Awake()
    {
        base.Awake();

        //Suscribe los eventos al manejador de eventos
        SubscribeEvents();

        LoadUserInfo();
        LoadSettingsInfo();
    }

    //Metodo de Unity que se ejecuta una sola vez antes del primer Update
    private void Start()
    {
        //La expresion que se encuentra a la derecha de la asignación es una comparación.
        //Compara si la escena actual es la última. Si la es, da true; si no, da false.
        _isLastScene = SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1;

        //Obtiene las configuraciones mismas de juego y setea las opciones de juego.

        _lastMusicState = _musicState;
        _lastSoundState = _soundState;

        _baseTime = Time.time;

        //Setea fecha y hora de inicio de la partida
        _attempt_Starting_Point = JsonUtility.ToJson((JsonDateTime)DateTime.Now);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventsManager.TriggerEvent("GP_PAUSE");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventsManager.TriggerEvent("GP_LEVELCOMPLETE");
        }

        RefreshGameTime();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        UnsubscribeEvents();
    }

    //cierre forzado
    protected override void OnApplicationQuit() 
    {
        base.OnApplicationQuit();
        //Agrega nuevo Attempt con crash en true. Descomentar antes de buildear juego (al trabajar es molesto)
        AddNewAttempt(false, true);
    }

    #endregion

    #region Subscripcion de eventos
    //Este metodo subscribe los metodos al manejador de eventos.
    private void SubscribeEvents()
    {
        EventsManager.SubscribeToEvent("GP_RESUME", SaveSettingsInfo);
        EventsManager.SubscribeToEvent("GP_LEVELCOMPLETE", UnlockLevel);
        EventsManager.SubscribeToEvent("GP_LEVELCOMPLETE", SaveUserInfo);
        EventsManager.SubscribeToEvent("GP_LEVELCOMPLETE", LevelCompletedAttempt);
        EventsManager.SubscribeToEvent("GP_LOSE", LoseAttempt);
    }
    private void UnsubscribeEvents()
    {
        EventsManager.UnsubscribeToEvent("GP_RESUME", SaveSettingsInfo);
        EventsManager.UnsubscribeToEvent("GP_LEVELCOMPLETE", UnlockLevel);
        EventsManager.UnsubscribeToEvent("GP_LEVELCOMPLETE", SaveUserInfo);
        EventsManager.UnsubscribeToEvent("GP_LEVELCOMPLETE", LevelCompletedAttempt);
        EventsManager.UnsubscribeToEvent("GP_LOSE", LoseAttempt);
    }
    #endregion

    //Guardo info de partida en lista de partidas
    public void LevelCompletedAttempt() => AddNewAttempt(true, false);
    public void LoseAttempt() => AddNewAttempt(false, false);

    #region Metodos privados
    //=============== PRIVATE METHODS =======================

    //Actualiza el tiempo de juego
    private void RefreshGameTime() => _gameTime = Time.time - _baseTime;

    private void AddNewAttempt(bool result, bool crash)
    {
        _listAttempts = _saveSystem.GetListAttempts();

        Attempt newAttempt = new Attempt();

        newAttempt.crashed = crash; //si la app crasheó o no

        //redundante?
        newAttempt.where_the_Game_Stopped = _currentSceneIndex; ////escena en la cual crasheó
        newAttempt.game_Level = _currentSceneIndex; //el nivel en el cual se desarrolló la partida 

        newAttempt.level_Completed = result; //derrota o victoria de la partida
        newAttempt.current_Game_Level = UserLevel; //último nivel desbloqueado por el usuario
        newAttempt.current_User_Level_In_The_Game = (int) (ExperiencePoints/10); //nivel de experiencia de usuario -> cada 10 puntos cambia de nivel
        newAttempt.experience_Points_per_Attempt = ExpPointsAttempt; //puntos de exp de la partida
        newAttempt.amount_of_Hits = HitsAttempt; 
        newAttempt.amount_of_Errors = ErrorsAttempt; 
        newAttempt.attempt_Starting_Point = Attempt_Starting_Point;
        newAttempt.attempt_End = JsonUtility.ToJson((JsonDateTime)DateTime.Now);
        newAttempt.attempt_Time = _gameTime;

        _listAttempts.list.Add(newAttempt);
        _saveSystem.SetListAttempts(_listAttempts);

        print("Partida guardada");
    }
    #endregion

    #region Eventos OnClick
    //=============== ONCLICK EVENTS ========================
    //Metodos destinados a botones
    public void OnClickPause()
    {
        EventsManager.TriggerEvent("GP_PAUSE");
    }
    public void OnClickResume()
    {
        EventsManager.TriggerEvent("GP_RESUME");
    }

    public void OnClickLevelComplete()
    {
        EventsManager.TriggerEvent("GP_LEVELCOMPLETE");
    }
    public void OnClickLose()
    {
        EventsManager.TriggerEvent("GP_LOSE");
    }
    #endregion
}