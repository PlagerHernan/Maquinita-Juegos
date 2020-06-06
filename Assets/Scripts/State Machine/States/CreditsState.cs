public class CreditsState : State<CreditsHandler>
{
    public CreditsState(GameManager gameManager, CreditsHandler handler) : base(gameManager, handler){}

    public override void OnEnter()
    {
        _handler.EnterMethods();
    }

    public override void OnExit()
    {
        _handler.ExitMethods();
    }

    public override void Tick()
    {
        if (_handler.BackToPause) { _gameManager.Credits = false; return; }
    }
}
