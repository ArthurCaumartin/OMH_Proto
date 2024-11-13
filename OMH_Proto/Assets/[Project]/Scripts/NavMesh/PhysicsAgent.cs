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
    [SerializeField] private float _fallingVelocity = -8;
    private NavMeshPath _navPath;
    private float _0to1Distance;
    private Rigidbody _rigidbody;

    private float _reComputePathTime;
    private Vector3[] _path = new Vector3[2];


    private void Start()
    {
        _navPath = new NavMeshPath();
        _rigidbody = GetComponent<Rigidbody>();
        GetNewPath();
    }

    private void FixedUpdate()
    {
        ComputePath();
        MoveRigidbody();
        DebugPath();   
    }

    private void MoveRigidbody()
    {
        if (!_target) return;

        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity
                                            , (_path[1] - transform.position).normalized * _speed
                                            , Time.deltaTime * _acceleration);
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _fallingVelocity, _rigidbody.velocity.z);
        // _rigidbody.AddForce((_path[1] - transform.position).normalized * _speed * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    private void DebugPath()
    {
        if(!DEBUG) return;
        for (int i = 0; i < _path.Length - 1; i++)
        {
            Debug.DrawLine(_path[i], _path[i + 1], Color.red);
        }
    }

    private void ComputePath()
    {
        if(_path.Length == 0)
        {
            _path = GetNewPath();
        }

        //! Si on est proche du point target on refait le path et on reset le timer
        if (Vector3.Distance(transform.position, _path[0]) > _0to1Distance)
        {
            _path = GetNewPath();
            return;
        }

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
            _navPath = new NavMeshPath();
            NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1000, NavMesh.AllAreas);
            _0to1Distance = Vector3.Distance(transform.position, hit.position);
            return new[] { transform.position, hit.position };
        }
    }

    public void SetTarget(Transform value)
    {
        _target = value;
        _path = GetNewPath();
    }
}
