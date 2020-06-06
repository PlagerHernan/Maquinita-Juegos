using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private StateMachine _stateMachine;
    private bool _paused;
    private bool _achievementsScreen;
    private Func<bool> _creditsToPause;
    private Func<bool> _pauseToCredits;

    public bool Paused { get; set; }
    public bool AchievementsScreen { get; set; }
    public bool Credits { get; set; }
    //public Func<bool> CreditsToPause { get ; set ; }
    //public Func<bool> PauseToCredits { get; set; }

    private void Awake()
    {
        //Se obtienen todos los componentes del GameObject propio
        var inGameHandler = GetComponent<InGameHandler>();
        var pauseHandler = GetComponent<PauseHandler>();
        var achievementsHandler = GetComponent<AchievementsHandler>();
        var winHandler = GetComponent<WinHandler>();
        var loseHandler = GetComponent<LoseHandler>();
        var creditsHandler = GetComponent<CreditsHandler>();

        //Se crea la maquina de estados
        _stateMachine = new StateMachine();

        //Se crean los estados, pasandoles por parametro el handler y el propio manager
        var inGameState         = new InGameState(this, inGameHandler);
        var pauseState          = new PauseState(this, pauseHandler);
        var achievemetnsState   = new AchievementsState(this, achievementsHandler);
        var winState            = new WinState(this, winHandler);
        var loseState           = new LoseState(this, loseHandler);
        var creditsState        = new CreditsState(this, creditsHandler);

        //Se setean las diferentes transiciones con sus métodos condicionantes (en el animator serían las condiciones para realizar la transicion).
        _stateMachine.AddTransition(inGameState, pauseState, PauseGame());
        _stateMachine.AddTransition(pauseState, inGameState, ResumeGame());
        _stateMachine.AddTransition(pauseState, achievemetnsState, WatchAchievements());
        _stateMachine.AddTransition(achievemetnsState, pauseState, AchievementsToPause());
        _stateMachine.AddTransition(pauseState, creditsState, PauseToCredits());
        _stateMachine.AddTransition(creditsState, pauseState, CreditsToPause());

        //Seteo un estado inicial
        _stateMachine.SetState(inGameState);

        //Algunos metodos condicionantes (tambien llamados lambdas)
        Func<bool> PauseGame() => () => Paused;
        Func<bool> ResumeGame() => () => !Paused;
        Func<bool> WatchAchievements() => () => AchievementsScreen;
        Func<bool> AchievementsToPause() => () => !AchievementsScreen;
        Func<bool> PauseToCredits() => () => Credits;
        Func<bool> CreditsToPause() => () => !Credits;
    }

    private void Update() => _stateMachine.Tick();
}