using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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
            Debug.Log("No target Set state to charge");
            _machinePteramyr.SetState(_machinePteramyr.ChargeState);
            return;
        }
        
        _agent.SetTarget(_machinePteramyr.Target);
        
        Collider[] col = Physics.OverlapSphere(_machinePteramyr.transform.position, _distanceToHitCharge.Value);
        for (int i = 0; i < col.Length; i++)
        {
            Health health = col[i].GetComponent<Health>();
            if (!health) continue;

            health.TakeDamages(_machinePteramyr.gameObject, 20);
            HitSomething();
        }
    }

    private void HitSomething()
    {
        _agent.Speed -= _speedtoLoseOnHit.Value;
        if(_agent.Speed < _speedToCharge.Value) _machinePteramyr.SetState(_machinePteramyr.PrepChargeState);
        else
        {
            _machinePteramyr.SetState(_machinePteramyr.ChargeState);
        }
    }

    public void ExitState()
    {
    }
}
