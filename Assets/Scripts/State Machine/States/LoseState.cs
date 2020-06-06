public class LoseState : State<LoseHandler>
{
    //private readonly GameManager _gameManager;
    //public LoseState(GameManager gameManager) => _gameManager = gameManager;
    public LoseState(GameManager gameManager, LoseHandler loseHandler) : base(gameManager, loseHandler) { }

    public override void OnEnter() { }
    public override void OnExit() { }
    public override void Tick() { }
}