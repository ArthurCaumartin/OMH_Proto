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
    private StateMachine_Pteramyr _machinePteramyr;

    public void Initialize(StateMachine behavior)
    {
        _agent = behavior.GetComponent<PhysicsAgent>();
        _patrolPoints = PatrolPoints.instance;
        _machinePteramyr = behavior as StateMachine_Pteramyr;
    }

    public void EnterState()
    {
        if (!_patrolPoints)
        {
            _machinePteramyr.SetState(_machinePteramyr.RoamState);
            return;
        }

        _partolTarget = _goOnFarestPoint ? _patrolPoints.GetFarsetPoint(_machinePteramyr.transform.position, _patrolPointPrecision)
                                             : _patrolPoints.GetRandomPoint(_patrolPointPrecision);
        _agent.SetTarget(_partolTarget);
    }

    public void UpdateState()
    {
        if (Vector3.Distance(_machinePteramyr.transform.position, _partolTarget) < 3)
        {
            _machinePteramyr.SetState(_machinePteramyr.RoamState);
            return;
        }
    }

    public void ExitState()
    {

    }
}
