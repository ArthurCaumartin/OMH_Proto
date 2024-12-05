using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Event", menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> _listeners = new List<GameEventListener>();
    [Tooltip("A string to explain why this event is here")]
    [SerializeField, TextArea] private string _infosAboutEvent;

    public void Raise(bool eventValue = true)
    {
        for (int i = 0; i < _listeners.Count; i++)
        {
            _listeners[i].OnEventRaise(eventValue);
        }
    }

    public void RegisterListener(GameEventListener listenerToAdd)
    {
        if (!_listeners.Contains(listenerToAdd))
            _listeners.Add(listenerToAdd);
    }

    public void UnRegisterListener(GameEventListener listenerToAdd)
    {
        if (_listeners.Contains(listenerToAdd))
            _listeners.Remove(listenerToAdd);
    }
}
