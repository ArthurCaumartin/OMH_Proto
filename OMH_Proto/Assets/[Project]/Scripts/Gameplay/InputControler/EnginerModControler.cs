using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnginerModControler : MonoBehaviour
{
    [SerializeField] private GameEvent _enginerModeEvent;
    private bool isOn;

    public void OnEnginerMod(InputValue value)
    {
        if (value.Get<float>() < .5f) return;
        isOn = !isOn;
        _enginerModeEvent.Raise(isOn);
    }
}
