using System;
using UnityEngine;

[Serializable]
public class State_Mob_Attack : IEntityState
{
    [SerializeField] private float _lookSpeed;
    [SerializeField] private FloatReference _distanceToTriggerAttack;
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
        // Debug.Log("ENTER ATTACK STATE");
        _timeDelay = _attackDelais.Value;
    }

    public void UpdateState(StateMachine behavior)
    {
        StateMachine_MobBase mobMachine = behavior as StateMachine_MobBase;

        if (!mobMachine.Target)
        {
            mobMachine.SetState(mobMachine.RoamState);
            return;
        }

        float _targetDistance = Vector3.Distance(mobMachine.transform.position, mobMachine.Target.position);
        if (_targetDistance > _distanceToTriggerAttack.Value)
        {
            mobMachine.SetState(mobMachine.RoamState);
            return;
        }

        bool canAttack = CheckTargetAlignement(mobMachine);
        if (!canAttack)
            return;


        // Debug.Log("Attack DoState");
        _timeDelay += Time.deltaTime;
        if (_timeDelay > _attackDelais.Value && _targetDistance < _distanceToTriggerAttack.Value)
        {
            // Debug.Log("Attack");
            _mobAnimationControler.PlayAttackAnimation(_attackAnimationSpeed.Value);
            _timeDelay = 0;
        }
    }

    private bool CheckTargetAlignement(StateMachine_MobBase mobMachine)
    {
        Vector3 targetDir = (mobMachine.Target.transform.position - mobMachine.transform.position).normalized;
        float dotDir = Vector3.Dot(mobMachine.transform.right, targetDir);
        if (dotDir < .95f)
        {
            mobMachine.transform.right = Vector3.Lerp(mobMachine.transform.right, targetDir, Time.deltaTime * _lookSpeed);
            _timeDelay = 0;
            return false;
        }

        return true;
    }

    public void ExitState(StateMachine behavior)
    {

    }
}
