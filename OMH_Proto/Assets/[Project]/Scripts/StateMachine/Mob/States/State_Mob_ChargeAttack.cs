using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class State_Mob_ChargeAttack : IEntityState
{
    [SerializeField] private FloatReference _distanceToChargeAt;
    [SerializeField] private FloatReference _distanceToHitCharge;
    [SerializeField] private FloatReference _speedToCharge;
    [SerializeField] private FloatReference _speedtoLoseOnHit;
    
    private PhysicsAgent _agent;
    private MobAnimationControler _mobAnimationControler;
    private StateMachine_Pterarmure _machinePteramyr;
    
    public void Initialize(StateMachine behavior)
    {
        _agent = behavior.GetComponent<PhysicsAgent>();
        _mobAnimationControler = behavior.GetComponentInChildren<MobAnimationControler>();
        _machinePteramyr = behavior as StateMachine_Pterarmure;
    }

    public void EnterState()
    {
        
    }

    public void UpdateState()
    {
        if (!_machinePteramyr.Target)
        {
            _machinePteramyr.SetState(_machinePteramyr.ChargeState);
            return;
        }
        
        _agent.SetTarget(_machinePteramyr.Target);
        
        float _targetDistance = Vector3.Distance(_machinePteramyr.transform.position, _machinePteramyr.Target.position);
        Debug.Log(_targetDistance);
        
        if (_targetDistance > _distanceToChargeAt.Value)
        {
            //Hit enemy
            Debug.Log("Hit Target");
            //Hit enemy
            _agent.Speed -= _speedtoLoseOnHit.Value;
            if(_agent.Speed < _speedToCharge.Value) _machinePteramyr.SetState(_machinePteramyr.PrepChargeState);
            else
            {
                _machinePteramyr.SetState(_machinePteramyr.ChargeState);
            }
            return;
        }
    }

    public void ExitState()
    {
    }
}
