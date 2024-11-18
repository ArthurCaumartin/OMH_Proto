using UnityEngine;
using UnityEngine.AI;

public class PhysicsAgent : MonoBehaviour
{
    public bool DEBUG = true;
    //TODO add un delais pour la recalculation du path
    [SerializeField] private Transform _target;
    [SerializeField] private float _reComputePathPerSecond = .5f;
    [Space]
    [SerializeField] private float _speed = 20;
    [SerializeField] private float _acceleration = 5;
    [SerializeField] private float _fallingVelocity = 0;
    private float _0to1Distance;
    private Rigidbody _rigidbody;

    private float _reComputePathTime;
    private Vector3[] _path = new Vector3[2];


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_target) GetNewPath();
    }

    private void FixedUpdate()
    {
        if (!_target)
        {
            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.zero, Time.fixedDeltaTime * 2);
            return;
        }
        ComputePath();
        MoveRigidbody();
        DebugPath();
    }

    private void MoveRigidbody()
    {
        Vector3 direction = (_path[1] - transform.position).normalized;
        transform.right = Vector3.Lerp(transform.right, new Vector3(direction.x, 0, direction.z), Time.deltaTime * 5);
        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity
                                            , direction * _speed
                                            , Time.deltaTime * _acceleration);
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _fallingVelocity, _rigidbody.velocity.z);
    }

    private void DebugPath()
    {
        if (!DEBUG) return;
        for (int i = 0; i < _path.Length - 1; i++)
        {
            Debug.DrawLine(_path[i], _path[i + 1], Color.red);
        }
    }

    private void ComputePath()
    {
        if (_path.Length == 0)
        {
            _path = GetNewPath();
        }

        //! Si on est proche du point target on refait le path et on reset le timer
        if (Vector3.Distance(transform.position, _path[0]) > _0to1Distance)
        {
            _path = GetNewPath();
            return;
        }

        //TODO OPTI nerf le refresh en fonction du nombre de phys agent
        _reComputePathTime += Time.deltaTime;
        if (_reComputePathTime > 1 / _reComputePathPerSecond)
        {
            _path = GetNewPath();
        }
    }

    private Vector3[] GetNewPath()
    {
        _reComputePathTime = 0;
        NavMeshPath newNavPath = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, _target.position, NavMesh.AllAreas, newNavPath))
        {
            _0to1Distance = Vector3.Distance(newNavPath.corners[0], newNavPath.corners[1]);
            return newNavPath.corners;
        }
        else
        {
            NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1000, NavMesh.AllAreas);
            _0to1Distance = Vector3.Distance(transform.position, hit.position);
            return new[] { transform.position, hit.position };
        }
    }

    public void SetTarget(Transform value)
    {
        // print("Set target to " + value);
        _target = value;
        if (_target) _path = GetNewPath();
    }
}
