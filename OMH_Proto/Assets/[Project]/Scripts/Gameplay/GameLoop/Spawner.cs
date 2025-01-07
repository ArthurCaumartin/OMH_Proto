using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private PhysicsAgent _agent;
    [Space]
    [SerializeField] private int _count = 5;
    [SerializeField] private float _spawnPerSecond = 2;
    private float _time;
    private int _currentCount;

    private void Update()
    {
        if (!_agent) return;
        _time += Time.deltaTime;
        if (_time > 1 / _spawnPerSecond)
        {
            _time = 0;
            if(_currentCount >= _count) return;
            _currentCount++;
            PhysicsAgent a = Instantiate(_agent, transform.TransformPoint(new Vector3(Random.Range(-1f, 1f), .5f, Random.Range(-1f, 1f))), Quaternion.identity);
            a.SetTarget(_target);
            a.transform.SetParent(transform);
        }
    }
}
