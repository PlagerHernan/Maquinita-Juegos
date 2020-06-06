public class PauseState : State<PauseHandler>
{
    public PauseState(GameManager gameManager, PauseHandler pauseHandler) : base(gameManager, pauseHandler) { }

    public override void OnEnter() => _handler.EnterMethods();
    public override void OnExit() => _handler.ExitMethods();
    public override void Tick()
    {
        if (_handler.Resume) { _gameManager.Paused = false; return; }
        if (_handler.WatchAchievements) _gameManager.AchievementsScreen = true;

        if(_handler.PauseToCredits) { _gameManager.Credits = true; return; }
    }
}
