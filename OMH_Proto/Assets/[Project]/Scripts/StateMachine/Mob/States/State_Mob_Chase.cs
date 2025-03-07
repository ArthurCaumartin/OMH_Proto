using System;
using UnityEngine;

[Serializable]
public class State_Mob_Chase : IEntityState
{
    [SerializeField] private float _distanceToTriggerAttack;
    private PhysicsAgent _agent;
    private StateMachine_Pteramyr _machinePteramyr;

    public void Initialize(StateMachine behavior)
    {
        _agent = behavior.GetComponent<PhysicsAgent>();
        _machinePteramyr = behavior as StateMachine_Pteramyr;
    }

    public void EnterState()
    {

    }

    public void UpdateState()
    {
        if (!_machinePteramyr.Target) 
        {
            _machinePteramyr.SetState(_machinePteramyr.RoamState);
            return;
        }

        _agent.SetTarget(_machinePteramyr.Target);

        if (Vector3.Distance(_machinePteramyr.transform.position, _machinePteramyr.Target.transform.position) <= _distanceToTriggerAttack)
        {
            _machinePteramyr.SetState(_machinePteramyr.AttackState);
        }
    }

    public void ExitState()
    {

    }
}
