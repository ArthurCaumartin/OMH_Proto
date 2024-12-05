using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool> _event;

    public void TriggerEvent(int value)
    {
        _event.Invoke(value != 0);
    }
}
