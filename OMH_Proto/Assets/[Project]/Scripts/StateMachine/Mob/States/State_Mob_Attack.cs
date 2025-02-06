using System;
using UnityEngine;

[Serializable]
public class State_Mob_Attack : IEntityState
{
    [SerializeField] private FloatReference _attackAnimationSpeed;
    [SerializeField] private FloatReference _attackDelais;
    private MobAnimationControler _mobAnimationControler;
    private float _timeDelay = 0;

    public void Initialize(StateMachine behavior)
    {
        _mobAnimationControler = behavior.GetComponentInChildren<MobAnimationControler>();
    }

    public void EnterState(StateMachine behavior)
    {
        Debug.Log("ENTER ATTACK STATE");
        _timeDelay = _attackDelais.Value;
    }

    public void DoState(StateMachine behavior)
    {
        StateMachine_MobBase mobMachine = behavior as StateMachine_MobBase;

        if(!mobMachine.Target) mobMachine.SetState(mobMachine.RoamState);

        Debug.Log("Attack DoState");
        _timeDelay += Time.deltaTime;
        if(_timeDelay > _attackDelais.Value)
        {
            Debug.Log("Attack");
            _mobAnimationControler.PlayAttackAnimation(_attackAnimationSpeed.Value);
            _timeDelay = 0;
        }
    }

    public void ExitState(StateMachine behavior)
    {

    }
}
