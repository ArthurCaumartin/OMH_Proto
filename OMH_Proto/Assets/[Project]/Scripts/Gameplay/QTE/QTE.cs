using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class QTE : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool> _onInputEvent;
    [SerializeField] private UnityEvent _onQTEWin;
    [SerializeField] private UnityEvent _onQTEKill;

    private List<Vector2> _directionSequence = new List<Vector2>();
    private int _index = 0;
    private QTEUI _qteUi;

    private void Start()
    {
        _qteUi = GetComponent<QTEUI>();
    }

    public void StartQTE(List<Vector2> directionSequence)
    {
        _index = 0;
        _directionSequence = directionSequence;
        _qteUi.ActivateUI(_directionSequence);
    }

    private void PlayInput(Vector2 inputDirection)
    {
        if (_directionSequence[_index] == inputDirection)
        {
            if (_index + 1 > _directionSequence.Count)
            {
                
                return;
            }

            _onInputEvent.Invoke(true);
            _index++;
        }
        else
        {
            _onInputEvent.Invoke(false);
        }
    }

    private void OnInputDirection(InputValue value)
    {
        Vector2 valueVector = value.Get<Vector2>();
        if (valueVector == Vector2.zero) return;
        PlayInput(valueVector);
    }
}
