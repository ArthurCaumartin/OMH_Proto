using System;
using UnityEngine;

[Serializable]
public class State_Mob_Chase : IEntityState
{
    [SerializeField] private float _distanceToTriggerAttack;
    private PhysicsAgent _agent;
    private StateMachine_MobBase _mobMachine;

    public void Initialize(StateMachine behavior)
    {
        _agent = behavior.GetComponent<PhysicsAgent>();
        _mobMachine = behavior as StateMachine_MobBase;
    }

    public void EnterState()
    {

    }

    public void UpdateState()
    {
        if (!_mobMachine.Target) 
        {
            _mobMachine.SetState(_mobMachine.RoamState);
            return;
        }

        _agent.SetTarget(_mobMachine.Target);

        if (Vector3.Distance(_mobMachine.transform.position, _mobMachine.Target.transform.position) <= _distanceToTriggerAttack)
        {
            _mobMachine.SetState(_mobMachine.AttackState);
        }
    }

    public void ExitState()
    {

    }
}
