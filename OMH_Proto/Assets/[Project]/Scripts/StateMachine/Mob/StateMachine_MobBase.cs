using UnityEngine;

public class StateMachine_MobBase : StateMachine
{
    [SerializeField] private Transform _target;
    [SerializeField] private State_Mob_Roam _roamState = new State_Mob_Roam();
    [SerializeField] private State_Mob_Spine _spineState = new State_Mob_Spine();
    [SerializeField] private State_Mob_Chase _chaseState = new State_Mob_Chase();
    private MobTargetFinder _targetFinder;

    public Transform Target { get => _target; }
    public State_Mob_Roam RoamState { get => _roamState; }
    public State_Mob_Spine SpineState { get => _spineState; }
    public State_Mob_Chase ChaseState { get => _chaseState; }

    private void Start()
    {
        // print("MobBase Start : Sub to targer Event");
        _targetFinder = GetComponent<MobTargetFinder>();

        // print("MobBase Start : Initialize State");
        _roamState.Initialize(this);
        _spineState.Initialize(this);
        _chaseState.Initialize(this);

        _currentState = _roamState;
    }

    private void Update()
    {
        _target = _targetFinder.Target;
        if (_target) SetState(_chaseState);
        PlayCurrentState();
    }

    private void PlayCurrentState()
    {
        if (_currentState == null) return;
        _currentState.DoState(this);
    }
}
