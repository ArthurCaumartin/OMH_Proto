using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class State_Mob_PrepCharge : IEntityState
{
    [SerializeField] private FloatReference _distanceToTriggerAttack;
    [SerializeField] private FloatReference _timeBetweenSpeedUp;
    [SerializeField] private FloatReference _speedToCharge;

    private PhysicsAgent _agent;
    StateMachine_Pterarmure _machinePteramyr;

    private float _timerSpeed;

    public void Initialize(StateMachine behavior)
    {
        _agent = behavior.GetComponent<PhysicsAgent>();
        _machinePteramyr = behavior as StateMachine_Pterarmure;
    }

    public void EnterState()
    {
        _timerSpeed = 0;
        _agent.Speed = 3;
    }

    public void UpdateState()
    {
        if (!_machinePteramyr.Target) return;
        _timerSpeed += Time.deltaTime;
        if (_timerSpeed > _timeBetweenSpeedUp.Value)
        {
            _timerSpeed = 0;
            _agent.Speed++;
            if (_agent.Speed >= _speedToCharge.Value)
            {
                _machinePteramyr.SetState(_machinePteramyr.ChargeState);
                return;
            }
        }

        _agent.SetTarget(_machinePteramyr.Target);

        if (Vector3.Distance(_machinePteramyr.transform.position, _machinePteramyr.Target.transform.position) <= _distanceToTriggerAttack.Value)
        {
            _machinePteramyr.SetState(_machinePteramyr.AttackState);
            return;
        }
    }

    public void ExitState()
    {

    }
}
