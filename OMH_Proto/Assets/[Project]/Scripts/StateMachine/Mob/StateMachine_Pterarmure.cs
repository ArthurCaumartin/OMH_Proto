using UnityEngine;

public class StateMachine_Pterarmure : StateMachine_MobBase
{
    [SerializeField] private State_Mob_Roam _roamState = new State_Mob_Roam();
    [SerializeField] private State_Mob_Chase _chaseState = new State_Mob_Chase();
    [SerializeField] private State_Mob_Attack _attackState = new State_Mob_Attack();

    public State_Mob_Roam RoamState { get => _roamState; }
    public State_Mob_Chase ChaseState { get => _chaseState; }
    public State_Mob_Attack AttackState { get => _attackState; }

    private void Start()
    {
        _roamState.Initialize(this);
        _chaseState.Initialize(this);
        _attackState.Initialize(this);

        SetState(RoamState);
    }
}
