using System;
using UnityEngine;

[Serializable]
public class State_Mob_Chase : IEntityState
{
    [SerializeField] private float _distanceToTriggerAttack;
    private PhysicsAgent _agent;

    public void Initialize(StateMachine behavior)
    {
        _agent = behavior.GetComponent<PhysicsAgent>();
    }

    public void EnterState(StateMachine behavior)
    {

    }

    public void DoState(StateMachine behavior)
    {
        StateMachine_MobBase mobMachine = behavior as StateMachine_MobBase;
        if (!mobMachine.Target) 
        {
            mobMachine.SetState(mobMachine.RoamState);
            return;
        }

        _agent.SetTarget(mobMachine.Target);

        if (Vector3.Distance(mobMachine.transform.position, mobMachine.Target.transform.position) <= _distanceToTriggerAttack)
        {
            mobMachine.SetState(mobMachine.AttackState);
        }
    }

    public void ExitState(StateMachine behavior)
    {

    }
}
