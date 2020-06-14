using System.Collections.Generic;

public class EventsManager
{
    public delegate void EventReceiver();
    private static Dictionary<string, EventReceiver> _events;

    /// <summary>
    /// Llamamos a este método para suscribirnos a eventos
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public static void SubscribeToEvent(string eventName, EventReceiver listener)
    {
        if (_events == null)
            _events = new Dictionary<string, EventReceiver>();

        if (!_events.ContainsKey(eventName))
            _events.Add(eventName, null);

        _events[eventName] += listener;
    }

    /// <summary>
    /// Llamamos a este método para desuscribirnos de eventos
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public static void UnsubscribeToEvent(string eventName, EventReceiver listener)
    {
        if (_events != null)
        {
            if (_events.ContainsKey(eventName))
                _events[eventName] -= listener;
        }
    }

    /// <summary>
    /// Llamamos a esta función para disparar un evento
    /// </summary>
    /// <param name="eventName"></param>
    public static void TriggerEvent(string eventName)
    {
        //TriggerEvent(eventType, null);
        if(_events == null)
        {
            UnityEngine.Debug.LogWarning("No events subscribed");
            return;
        }

        if (_events.ContainsKey(eventName))
            _events[eventName]?.Invoke();
    }

    /// <summary>
    /// Dispara el evento
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="parametersWrapper"></param>
    //public static void TriggerEvent(string eventName, params object[] parametersWrapper)
    //{
    //    if (_events == null)
    //    {
    //        UnityEngine.Debug.LogWarning("No events subscribed");
    //        return;
    //    }

    //    if (_events.ContainsKey(eventName))
    //    {
    //        if (_events[eventName] != null)
    //            _events[eventName](parametersWrapper);
    //    }
    //}
}