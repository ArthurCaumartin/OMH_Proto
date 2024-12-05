using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent _event;
    [SerializeField] private UnityEvent<bool> _response;
    private List<Action> _actionList;




    private void OnEnable() { _event.RegisterListener(this); }
    private void OnDisable() { _event.UnRegisterListener(this); }

    public void OnEventRaise(bool eventValue)
    {
        _response.Invoke(eventValue);

        for (int i = 0; i < _actionList.Count; i++)
            _actionList[i].Invoke();
    }

    public void SubAction(Action action)
    {
        if (!_actionList.Contains(action)) _actionList.Add(action);
    }
}