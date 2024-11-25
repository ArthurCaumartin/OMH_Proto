using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QTE : MonoBehaviour
{
    [SerializeField] private GameEvent _onQTEStart;
    [SerializeField] private GameEvent _onQTEEnd;
    private int _currentIndex = 0;
    private List<Vector2> _currentDirectionSequence = new List<Vector2>();

    public void Play(List<Vector2> directionSequence)
    {
        _currentDirectionSequence = directionSequence;
    }

    private void PlayInput(Vector2 inputDirection)
    {
        _toDoInInput.Invoke(true);
    }

    private void OnInputDirection(InputValue value)
    {
        Vector2 valueVector = value.Get<Vector2>();
        if (valueVector == Vector2.zero) return;
        PlayInput(valueVector);
    }
}
