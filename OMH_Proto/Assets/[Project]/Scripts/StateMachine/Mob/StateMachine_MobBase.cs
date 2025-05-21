using System.Collections;
using UnityEngine;

public class StateMachine_MobBase : StateMachine
{
    // public float debugDistance;
    private Transform _target;
    private MobTargetFinder _targetFinder;
    private PhysicsAgent _agent;
    private bool _isStun = false;
    private MobAnimationControler _animationControler;

    public Transform Target { get => _target; }

    private void Awake()
    {
        _animationControler = GetComponentInChildren<MobAnimationControler>();
        _targetFinder = GetComponent<MobTargetFinder>();
        _agent = GetComponent<PhysicsAgent>();
        _isStun = false;
    }

    public virtual void Update()
    {
        if (_isStun) return;
        _target = _targetFinder.Target;
        // debugDistance = _target ? Vector3.Distance(transform.position, _target.position) : -1;
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
        _isStun = true;
        _agent.SlowAgent(1, duration, true);
        StartCoroutine(ResetStun(duration));
        _animationControler.PlayStunAnimationn(duration);
    }

    private IEnumerator ResetStun(float duration)
    {
        yield return new WaitForSeconds(duration);
        _isStun = false;
    }
}
