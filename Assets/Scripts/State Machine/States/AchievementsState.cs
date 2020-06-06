public class AchievementsState : State<AchievementsHandler>
{
    public AchievementsState(GameManager gameManager, AchievementsHandler achievementsHandler) : base(gameManager, achievementsHandler) { }

    public override void OnEnter() => _handler.EnterMethods();
    public override void OnExit() => _handler.ExitMethods();
    public override void Tick()
    {
        if (_handler.BackToPause) _gameManager.AchievementsScreen = false;
    }
}
