using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class State_Mob_Charge : IEntityState
{
    private PhysicsAgent _agent;
    StateMachine_Pterarmure _machinePteramyr;
    

    public void Initialize(StateMachine behavior)
    {
        _agent = behavior.GetComponent<PhysicsAgent>();
        _machinePteramyr = behavior as StateMachine_Pterarmure;
    }

    public void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState()
    {
        _agent.SetTarget(_machinePteramyr.Target);
    }

    public void ExitState()
    {
        throw new System.NotImplementedException();
    }
}
