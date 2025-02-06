using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent _event;
    public UnityEvent Event { get => _event; }

    public void TriggerEvent()
    {
        _event.Invoke();
    }
}
