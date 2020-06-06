public class InGameState : State<InGameHandler>
{
    public InGameState(GameManager gameManager, InGameHandler inGameHandler) : base(gameManager, inGameHandler) { }

    public override void OnEnter() => _handler.EnterMethods();
    public override void OnExit() => _handler.ExitMethods();
    public override void Tick()
    {
        if (_handler.Paused) _gameManager.Paused = true;
    }
}