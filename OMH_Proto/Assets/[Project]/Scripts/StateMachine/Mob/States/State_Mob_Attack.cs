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
    private StateMachine_Pteramyr _machinePteramyr;
    private float _timeDelay = 0;
    private PhysicsAgent _physicAgent;

    public void Initialize(StateMachine behavior)
    {
        _mobAnimationControler = behavior.GetComponentInChildren<MobAnimationControler>();
        _machinePteramyr = behavior as StateMachine_Pteramyr;
        _physicAgent = _machinePteramyr.GetComponent<PhysicsAgent>();
    }

    public void EnterState()
    {
        // Debug.Log("ENTER ATTACK STATE");
        _timeDelay = _attackDelais.Value;

        _physicAgent.enabled = false;
    }

    public void UpdateState()
    {
        if (!_machinePteramyr.Target)
        {
            _machinePteramyr.SetState(_machinePteramyr.RoamState);
            return;
        }

        float _targetDistance = Vector3.Distance(_machinePteramyr.transform.position, _machinePteramyr.Target.position);
        if (!IsTargetAlign() | _targetDistance > _distanceToTriggerAttack.Value)
        {
            _machinePteramyr.SetState(_machinePteramyr.ChaseState);
            return;
        }


        // Debug.Log("Attack DoState");
        _timeDelay += Time.deltaTime;
        if (_timeDelay > _attackDelais.Value && _targetDistance < _distanceToTriggerAttack.Value)
        {
            // Debug.Log("Attack");
            _mobAnimationControler.PlayAttackAnimation(_attackAnimationSpeed.Value);
            _timeDelay = 0;
        }
    }

    private bool IsTargetAlign()
    {
        Vector3 targetDir = (_machinePteramyr.Target.transform.position - _machinePteramyr.transform.position).normalized;
        float dotDir = Vector3.Dot(_machinePteramyr.transform.right, targetDir);
        Debug.Log("dot : " + dotDir);
        return dotDir > .95f;
    }

    public void ExitState()
    {
        // Debug.Log("EXIT ATTACK STATE");
        _physicAgent.enabled = true;
    }
}
