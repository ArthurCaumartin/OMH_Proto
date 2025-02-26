using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class State_Mob_Roam : IEntityState
{
    [SerializeField] private float _delay = 5;
    [SerializeField] private float _maxCount = 5;
    [SerializeField] private float _precision = 3;
    private float _timer;
    private Vector3 _startPos;
    private PhysicsAgent _agent;
    private Vector3 _randomPos;
    private int _count;

    public void Initialize(StateMachine behavior)
    {
        _agent = behavior.GetComponent<PhysicsAgent>();
    }

    public void EnterState(StateMachine behavior)
    {
        _startPos = behavior.transform.position;
        _randomPos = GetRandomPos();
    }

    public void UpdateState(StateMachine behavior)
    {
        StateMachine_MobBase mobMachine = behavior as StateMachine_MobBase;

        if (mobMachine.Target)
        {
            mobMachine.SetState(mobMachine.ChaseState);
            return;
        }

        _timer += Time.deltaTime;
        if (_timer > _delay)
        {
            _timer = 0;
            _randomPos = GetRandomPos();
            _agent.SetTarget(_randomPos);
            _count++;
        }

        if (_count > _maxCount)
        {
            _count = 0;
            mobMachine.SetState(mobMachine.RoamState);
        }
    }

    public void ExitState(StateMachine behavior)
    {
        _agent.ClearTarget();
    }

    private Vector3 GetRandomPos()
    {
        return _startPos + new Vector3(Random.Range(-_precision, _precision), 0, Random.Range(-_precision, _precision));
    }

}
