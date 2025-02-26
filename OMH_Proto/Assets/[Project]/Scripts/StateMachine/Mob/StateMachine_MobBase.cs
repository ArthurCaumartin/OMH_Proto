using System.Collections;
using UnityEngine;

public class StateMachine_MobBase : StateMachine
{
    [SerializeField] private Transform _target;
    [SerializeField] private State_Mob_Roam _roamState = new State_Mob_Roam();
    [SerializeField] private State_Mob_Patrol _patrolState = new State_Mob_Patrol();
    [SerializeField] private State_Mob_Chase _chaseState = new State_Mob_Chase();
    [SerializeField] private State_Mob_Attack _attackState = new State_Mob_Attack();
    private MobTargetFinder _targetFinder;

    public Transform Target { get => _target; }

    public State_Mob_Roam RoamState { get => _roamState; }
    public State_Mob_Chase ChaseState { get => _chaseState; }
    public State_Mob_Attack AttackState { get => _attackState; }
    public State_Mob_Patrol PatrolState { get => _patrolState; }

    private bool _isOn = true;
    private PhysicsAgent _agent;

    private void Start()
    {
        // print("MobBase Start : Sub to targer Event");
        _targetFinder = GetComponent<MobTargetFinder>();
        _agent = GetComponent<PhysicsAgent>();

        // print("MobBase Start : Initialize State");
        _roamState.Initialize(this);
        _patrolState.Initialize(this);
        _chaseState.Initialize(this);
        _attackState.Initialize(this);

        SetState(RoamState);
    }

    private void Update()
    {
        if (!_isOn) return;
        _target = _targetFinder.Target;
        PlayCurrentState();
    }

    private void PlayCurrentState()
    {
        // print("Current State : " + _currentState?.ToString());
        if (_currentState == null) return;
        _currentState.UpdateState(this);
    }

    public void StunMob(float duration)
    {
        print("Mob Stun");
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
