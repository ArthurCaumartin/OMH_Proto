using System;
using UnityEngine;

[Serializable]
public class State_Mob_Patrol : IEntityState
{
    [SerializeField] private bool _goOnFarestPoint = true;
    [SerializeField] private float _patrolPointPrecision;
    private PhysicsAgent _agent;
    private PatrolPoints _patrolPoints;
    private Vector3 _partolTarget;

    public void Initialize(StateMachine behavior)
    {
        _agent = behavior.GetComponent<PhysicsAgent>();
        _patrolPoints = PatrolPoints.instance;
    }

    public void EnterState(StateMachine behavior)
    {
        StateMachine_MobBase mobMachine = behavior as StateMachine_MobBase;
        if (!_patrolPoints)
        {
            mobMachine.SetState(mobMachine.RoamState);
            return;
        }

        _partolTarget = _goOnFarestPoint ? _patrolPoints.GetFarsetPoint(behavior.transform.position, _patrolPointPrecision)
                                             : _patrolPoints.GetRandomPoint(_patrolPointPrecision);
        _agent.SetTarget(_partolTarget);
    }

    public void UpdateState(StateMachine behavior)
    {
        if (Vector3.Distance(behavior.transform.position, _partolTarget) < 3)
        {
            StateMachine_MobBase mobMachine = behavior as StateMachine_MobBase;
            mobMachine.SetState(mobMachine.RoamState);
            return;
        }
    }

    public void ExitState(StateMachine behavior)
    {

    }
}
