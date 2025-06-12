using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class State_Mob_PrepCharge : IEntityState
{
    [SerializeField] private FloatReference _distanceToTriggerAttack;
    [SerializeField] private FloatReference _timeBetweenSpeedUp;
    [SerializeField] private FloatReference _speedToCharge;
    [SerializeField] private FloatReference _maxSpeed;


    private PhysicsAgent _agent;
    StateMachine_Pterarmure _machinePteramyr;
    private MobAnimationControler _animationControler;
    private float _startSpeedBackup;
    private float _timerSpeed;
    private float _transitionTime;

    public void Initialize(StateMachine behavior)
    {
        _agent = behavior.GetComponent<PhysicsAgent>();
        _machinePteramyr = behavior as StateMachine_Pterarmure;
        _animationControler = _machinePteramyr.GetComponentInChildren<MobAnimationControler>();
        _startSpeedBackup = _agent.SpeedBaseMultiplier;
    }

    public void EnterState()
    {
        _timerSpeed = 0;
        _agent.Speed = 3;
        _agent.CanBeSlow = true;
    }

    public void UpdateState()
    {
        if (!_machinePteramyr.Target) return;
        _animationControler.SetWalkTransition(Mathf.InverseLerp(_startSpeedBackup, _maxSpeed.Value, _agent.Speed));
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
