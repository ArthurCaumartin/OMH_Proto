using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PhysicsAgent : MonoBehaviour
{
    //TODO add un delais pour la recalculation du path
    [SerializeField] private float _reComputePathPerSecond = .5f;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 20;
    [SerializeField] private float _acceleration = 5;
    private NavMeshPath _navPath;
    private Rigidbody _rigidbody;

    private float _reConputePathTime;

    private void Start()
    {
        _navPath = new NavMeshPath();
        _rigidbody = GetComponent<Rigidbody>();
        GetNewPath();
    }

    private void Update()
    {
        ComputePath();

        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity
                                            , (_navPath.corners[1] - _navPath.corners[0]).normalized * _speed
                                            , Time.deltaTime * _acceleration);

        for (int i = 0; i < _navPath.corners.Length - 1; i++)
        {
            Debug.DrawLine(_navPath.corners[i], _navPath.corners[i + 1], Color.red);
        }
    }

    private void ComputePath()
    {
        if (_navPath != null)
        {
            //! Si on est proche du point target on refait le path et on reset le timer
            if (Vector3.Distance(transform.position, _navPath.corners[1]) < 1)
            {
                _reConputePathTime = 0;
                GetNewPath();
                return;
            }
        }

        _reConputePathTime += Time.deltaTime;
        if (_reConputePathTime > 1 / _reComputePathPerSecond)
        {
            _reConputePathTime = 0;
            GetNewPath();
        }
    }

    private void GetNewPath()
    {
        NavMesh.CalculatePath(transform.position, _target.position, NavMesh.AllAreas, _navPath);
        print(_navPath.corners.Length);
    }
}
