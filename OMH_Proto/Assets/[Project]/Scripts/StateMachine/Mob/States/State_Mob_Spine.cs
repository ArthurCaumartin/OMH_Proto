using System;
using UnityEngine;


[Serializable]
public class State_Mob_Spine : IEntityState
{
    [SerializeField] private float _timer = 1f;
    [SerializeField] private float _speed = 10f;
    private float currentTime;
    private PhysicsAgent _agent;

    public void Initialize(StateMachine behavior)
    {
        _agent = behavior.GetComponent<PhysicsAgent>();
    }

    public void DoState(StateMachine behavior)
    {
        StateMachine_MobBase mobMachine = behavior as StateMachine_MobBase;
        currentTime += Time.deltaTime;
        mobMachine.transform.Rotate(0, 500 * Time.deltaTime * _speed, 0);
        if (currentTime > _timer)
        {
            currentTime = 0;
            mobMachine.SetState(mobMachine.RoamState);
        }
    }

    public void EnterState(StateMachine behavior)
    {
        _agent.ClearTarget();
    }

    public void ExitState(StateMachine behavior)
    {

    }
}


public class State_Mob_Attack : IEntityState
{
    public void DoState(StateMachine behavior)
    {
        throw new NotImplementedException();
    }

    public void EnterState(StateMachine behavior)
    {
        throw new NotImplementedException();
    }

    public void ExitState(StateMachine behavior)
    {
        throw new NotImplementedException();
    }

    public void Initialize(StateMachine behavior)
    {
        throw new NotImplementedException();
    }
}
