public interface IState
{
    /// <summary>
    /// Metodo que chequea si hay que hacer alguna transicion
    /// </summary>
    void Tick();
    /// <summary>
    /// Evento que se triggerea cuando se inicializa un estado
    /// </summary>
    void OnEnter();
    /// <summary>
    /// Evento que se triggerea antes de salir de un estado
    /// </summary>
    void OnExit();
}