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
    StateMachine_Pteramyr _machinePteramyr;

    public void Initialize(StateMachine behavior)
    {
        _agent = behavior.GetComponent<PhysicsAgent>();
        _machinePteramyr = behavior as StateMachine_Pteramyr;
    }

    public void EnterState()
    {
        _agent.ClearTarget();
        _startPos = _machinePteramyr.transform.position;
        _randomPos = GetRandomPos();
    }

    public void UpdateState()
    {
        if (_machinePteramyr.Target)
        {
            _machinePteramyr.SetState(_machinePteramyr.ChaseState);
            return;
        }

        if (Vector3.Distance(_machinePteramyr.transform.position, _randomPos) <= 1f)
        {
            _agent.ClearTarget();
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
            _machinePteramyr.SetState(_machinePteramyr.RoamState);
        }
    }

    public void ExitState()
    {
        _agent.ClearTarget();
    }

    private Vector3 GetRandomPos()
    {
        return _startPos + new Vector3(Random.Range(-_precision, _precision), 0, Random.Range(-_precision, _precision));
    }

}
