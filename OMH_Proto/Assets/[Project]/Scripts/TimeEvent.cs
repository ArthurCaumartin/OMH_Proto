using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeEvent : MonoBehaviour
{
    [Serializable]
    public class TimedEvent
    {
        public string name;
        public bool isAllreadyCall;
        [Space]
        public FloatReference timeToTrigger;
        public GameEvent gameEvent;
        public UnityEvent unityEvent;
    }

    [SerializeField] private FloatReference _gameTime;
    [SerializeField] private List<TimedEvent> _timedEventList = new List<TimedEvent>();


    private void Update()
    {
        _gameTime.Value += Time.deltaTime;
        foreach (var item in _timedEventList)
        {
            if (!item.isAllreadyCall & item.timeToTrigger.Value < _gameTime.Value)
            {
                item.unityEvent.Invoke();
                item.gameEvent?.Raise();
                item.isAllreadyCall = true;
            }
        }
    }
}
