using System.Collections;
using UnityEngine;

public class StateMachine_MobBase : StateMachine
{
    private Transform _target;
    private MobTargetFinder _targetFinder;
    private PhysicsAgent _agent;
    private bool _isOn = true;

    public Transform Target { get => _target; }

    private void Awake()
    {
        _targetFinder = GetComponent<MobTargetFinder>();
        _agent = GetComponent<PhysicsAgent>();
    }

    public virtual void Update()
    {
        if (!_isOn) return;
        _target = _targetFinder.Target;
        PlayCurrentState();
    }

    private void PlayCurrentState()
    {
        // print("Current State : " + _currentState?.ToString());
        if (_currentState == null) return;
        _currentState.UpdateState();
    }

    public void StunMob(float duration)
    {
        // print("Mob Stun");
        _isOn = false;
        _agent.SlowAgent(1, duration, true);
        StartCoroutine(ResetIsOn(duration));
    }

    private IEnumerator ResetIsOn(float duration)
    {
        yield return new WaitForSeconds(duration);
        _isOn = true;
    }
}
