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
    private StateMachine_MobBase _mobMachine;

    public void Initialize(StateMachine behavior)
    {
        _mobAnimationControler = behavior.GetComponentInChildren<MobAnimationControler>();
        _mobMachine = behavior as StateMachine_MobBase;
    }

    public void EnterState()
    {
        // Debug.Log("ENTER ATTACK STATE");
        _timeDelay = _attackDelais.Value;
    }

    public void UpdateState()
    {
        if (!_mobMachine.Target)
        {
            _mobMachine.SetState(_mobMachine.RoamState);
            return;
        }

        float _targetDistance = Vector3.Distance(_mobMachine.transform.position, _mobMachine.Target.position);
        if (_targetDistance > _distanceToTriggerAttack.Value)
        {
            _mobMachine.SetState(_mobMachine.RoamState);
            return;
        }

        if (!CheckTargetAlignement())
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

    private bool CheckTargetAlignement()
    {
        Vector3 targetDir = (_mobMachine.Target.transform.position - _mobMachine.transform.position).normalized;
        float dotDir = Vector3.Dot(_mobMachine.transform.right, targetDir);
        if (dotDir < .95f)
        {
            _mobMachine.transform.right = Vector3.Lerp(_mobMachine.transform.right, targetDir, Time.deltaTime * _lookSpeed);
            _timeDelay = 0;
            return false;
        }

        return true;
    }

    public void ExitState()
    {

    }
}
