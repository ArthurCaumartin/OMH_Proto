using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Event", menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    [Tooltip("A string to explain why this event is here")]
    [SerializeField, TextArea] private string _infosAboutEvent;

    //! la struct c pour l'affichage dans l'inspecteur
    [Serializable] public struct DialogueEvent { [TextArea] public string text; }
    [SerializeField] private DialogueEvent _dialogue;

    private List<GameEventListener> _listeners = new List<GameEventListener>();

    public void Raise(bool eventValue = true)
    {
        TryPrintDialogue();
        for (int i = 0; i < _listeners.Count; i++)
        {
            _listeners[i].OnEventRaise(eventValue);
        }
    }

    private void TryPrintDialogue()
    {
        if (Application.isPlaying && _dialogue.text != "")
        {
            // Debug.Log("Try call dialogue"); 
            DialogueBox.instance?.PrintNewDialogue(_dialogue.text);
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


