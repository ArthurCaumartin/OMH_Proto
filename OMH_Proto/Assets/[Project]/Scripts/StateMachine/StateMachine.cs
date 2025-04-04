using System.Security.Cryptography;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField, Tooltip("For debug :)")] private string _debugStateName;
    protected IEntityState _currentState;

    public virtual void SetState(IEntityState toSet)
    {
        if (_currentState == null)
        {
            _currentState = toSet;
            _debugStateName = _currentState.ToString();
            _currentState.EnterState();
            return;
        }

        if (toSet == _currentState) return;
        _currentState.ExitState();
        _currentState = toSet;
        _debugStateName = _currentState.ToString();
        _currentState.EnterState();
    }
}
