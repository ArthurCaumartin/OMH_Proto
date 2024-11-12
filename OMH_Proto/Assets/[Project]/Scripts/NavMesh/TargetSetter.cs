using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetSetter : MonoBehaviour
{
    public Transform _target;
    private NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(_target.position);
    }

    private void Update()
    {
        _agent.SetDestination(_target.position);
    }
}
