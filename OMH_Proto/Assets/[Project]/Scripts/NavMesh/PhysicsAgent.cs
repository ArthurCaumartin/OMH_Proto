using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private float _0to1Distance;
    private Rigidbody _rigidbody;

    private float _reConputePathTime;
    private Vector3[] _vectorPath;

    private void Start()
    {
        _navPath = new NavMeshPath();
        _rigidbody = GetComponent<Rigidbody>();
        GetNewPath();
    }

    private void Update()
    {
        ComputePath();

        if (_navPath.corners.Count() == 0)
        {
            return;
            // _navPath = new NavMeshPath();
            // NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1000, NavMesh.AllAreas);
            // _navPath.corners = new[] { transform.position, hit.position };
        }
        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity
                                            , (_navPath.corners[1] - transform.position).normalized * _speed
                                            , Time.deltaTime * _acceleration);
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, Physics.gravity.y, _rigidbody.velocity.z);

        for (int i = 0; i < _navPath.corners.Length - 1; i++)
        {
            Debug.DrawLine(_navPath.corners[i], _navPath.corners[i + 1], Color.red);
        }
    }

    private void ComputePath()
    {
        if (_navPath == null || _navPath.corners.Count() == 0)
        {
            GetNewPath();
            return;
        }

        //! Si on est proche du point target on refait le path et on reset le timer
        if (Vector3.Distance(transform.position, _navPath.corners[0]) > _0to1Distance)
        {
            GetNewPath();
            return;
        }

        _reConputePathTime += Time.deltaTime;
        if (_reConputePathTime > 1 / _reComputePathPerSecond)
        {
            GetNewPath();
        }
    }

    private void GetNewPath()
    {
        _reConputePathTime = 0;
        if (NavMesh.CalculatePath(transform.position, _target.position, NavMesh.AllAreas, _navPath))
            _0to1Distance = Vector3.Distance(_navPath.corners[0], _navPath.corners[1]);

    }

    public void SetTarget(Transform value)
    {
        _target = value;
    }
}
