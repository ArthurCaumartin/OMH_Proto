using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class State_Mob_Charge : IEntityState
{
    [SerializeField] private FloatReference _distanceToChargeAt;
    [SerializeField] private FloatReference _timeBetweenSpeedUp;
    [SerializeField] private FloatReference _maxSpeed;
    private float _startSpeedBackup;
    private PhysicsAgent _agent;
    StateMachine_Pterarmure _machinePteramyr;
    private MobAnimationControler _animationControler;
    
    private float _timerSpeed;

    public void Initialize(StateMachine behavior)
    {
        _agent = behavior.GetComponent<PhysicsAgent>();
        _machinePteramyr = behavior as StateMachine_Pterarmure;
        _animationControler = _machinePteramyr.GetComponentInChildren<MobAnimationControler>();
        _startSpeedBackup = _agent.SpeedBaseMultiplier;
    }

    public void EnterState()
    {
        _agent.CanBeSlow = false;
    }

    public void UpdateState()
    {
        _animationControler.SetWalkTransition(Mathf.InverseLerp(_startSpeedBackup, _maxSpeed.Value, _agent.Speed));
        _timerSpeed += Time.deltaTime;
        if (_timerSpeed > _timeBetweenSpeedUp.Value)
        {
            _timerSpeed = 0;
            if(_timerSpeed < _maxSpeed.Value) _agent.Speed++;
        }

        if (_machinePteramyr.Target == null) return;
        
        Debug.Log("target name : " + _machinePteramyr.Target.name);
        _agent.SetTarget(_machinePteramyr.Target);
        
        if (Vector3.Distance(_machinePteramyr.transform.position, _machinePteramyr.Target.transform.position) <= _distanceToChargeAt.Value)
        {
            _machinePteramyr.SetState(_machinePteramyr.ChargeAttackState);
            return;
        }
    }

    public void ExitState()
    {
        
    }
}
