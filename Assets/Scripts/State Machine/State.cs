public abstract class State<T> : IState
{
    protected readonly GameManager _gameManager;
    protected readonly T _handler;

    public State(GameManager gameManager, T handler)
    {
        _gameManager = gameManager;
        _handler = handler;
    }

    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void Tick();
}
