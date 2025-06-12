using System;
using System.Collections;
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
    private StateMachine_Pterarmure _machinePterarmure;


    public void Initialize(StateMachine behavior)
    {
        _agent = behavior.GetComponent<PhysicsAgent>();
        _mobAnimationControler = behavior.GetComponentInChildren<MobAnimationControler>();
        _machinePterarmure = behavior as StateMachine_Pterarmure;
    }

    public void EnterState()
    {
    }

    public void UpdateState()
    {
        // if (_mobAnimationControler.IsChargeAttack())
        // {
        //     _agent.SlowAgent(1, Time.deltaTime);
        //     return;
        // }

        if (!_machinePterarmure.Target)
        {
            // Debug.Log("No target Set state to charge");
            _machinePterarmure.SetState(_machinePterarmure.ChargeState);
            return;
        }

        _agent.SetTarget(_machinePterarmure.Target);

        Collider[] col = Physics.OverlapSphere(_machinePterarmure.transform.position, _distanceToHitCharge.Value);
        for (int i = 0; i < col.Length; i++)
        {
            Health health = col[i].GetComponent<Health>();
            if (!health) continue;

            health.TakeDamages(_machinePterarmure.gameObject, 20);
            HitSomething();
        }
    }

    private void HitSomething()
    {
        _agent.Speed = 0;
        _mobAnimationControler.SetWalkTransition(0);
        _mobAnimationControler.PlayChargeAttack(out float duration);
        _machinePterarmure.StartCoroutine(WaitForEndOfAnimation(duration));
    }

    private IEnumerator WaitForEndOfAnimation(float duration)
    {
        yield return new WaitForSeconds(duration - .8f);
        _machinePterarmure.SetState(_machinePterarmure.PrepChargeState);
    }

    public void ExitState()
    {

    }
}
