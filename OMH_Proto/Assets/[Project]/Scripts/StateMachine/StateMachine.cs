using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class StateMachine : MonoBehaviour
{
    [SerializeField, Tooltip("For debug :)")] private string _debugStateName;
    protected IEntityState _currentState;

    // call when state change, Par1 = The state we leaving / Par2 = The State we enter
    [HideInInspector] public UnityEvent<IEntityState, IEntityState> OnStateChange;

    public virtual void SetState(IEntityState toSet)
    {
        OnStateChange.Invoke(_currentState, toSet);
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
