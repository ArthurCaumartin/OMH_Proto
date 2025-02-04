using System;
using UnityEngine;

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
        _timeDelay = _attackDelais.Value;
    }

    public void DoState(StateMachine behavior)
    {
        _timeDelay += Time.deltaTime;
        if(_timeDelay > _attackDelais.Value)
        {
            _mobAnimationControler.PlayAttackAnimation();
            _timeDelay = 0;
        }
    }

    public void ExitState(StateMachine behavior)
    {

    }
}
