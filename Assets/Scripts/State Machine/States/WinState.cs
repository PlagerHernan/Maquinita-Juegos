public class WinState : State<WinHandler>
{
    //private readonly GameManager _gameManager;
    //public WinState(GameManager gameManager) => _gameManager = gameManager;
    public WinState(GameManager gameManager, WinHandler winHandler) : base(gameManager, winHandler) { }

    public override void OnEnter() { }
    public override void OnExit() { }
    public override void Tick() { }
}
