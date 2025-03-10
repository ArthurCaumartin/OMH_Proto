using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PhysicsAgent : MonoBehaviour
{
    public bool DEBUG = true;
    //TODO add un delais pour la recalculation du path
    [SerializeField] private float _reComputePathPerSecond = .5f;
    [Space]
    [SerializeField] private FloatReference _enemySpeed;
    private float _slowMultiplier = 1;
    [SerializeField] private float _acceleration = 5;
    [SerializeField] private float _rotationSpeed = 5;
    [SerializeField] private float _fallingVelocity = 0;
    [Space]
    private float _0to1Distance;
    private Rigidbody _rigidbody;
    private float _reComputePathTime;
    private Vector3[] _path = new Vector3[0];
    private Transform _currentTarget = null;
    private Vector3 _posToGoIfNoTarget = Vector3.zero;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetTarget(Transform tragetTransform)
    {
        // print("Set target : " + tragetTransform.name);
        if (tragetTransform == _currentTarget) return;
        _currentTarget = tragetTransform;
    }

    public void SetTarget(Vector3 posToGo)
    {
        _posToGoIfNoTarget = posToGo;
    }

    public void ClearTarget() //TODO swap with SetTarget(null) ?
    {
        _currentTarget = null;
        _path = new[] { Vector3.zero };
        _posToGoIfNoTarget = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (_currentTarget == null && _posToGoIfNoTarget == Vector3.zero)
        {
            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.zero, Time.fixedDeltaTime * _acceleration);
            return;
        }

        if (_currentTarget)
        {
            _path = ComputePath(_currentTarget.position);
            MoveRigidbody(_path);
            DebugPath();
            return;
        }

        if (_posToGoIfNoTarget != Vector3.zero)
        {
            _path = ComputePath(_posToGoIfNoTarget);
            MoveRigidbody(_path);
            DebugPath();
            return;
        }
    }

    private Vector3[] ComputePath(Vector3 targetPosition)
    {
        if (_path.Length == 0)
        {
            return GetNewPath(targetPosition);
        }

        //? Si on depasse le point [1] on refait le path et on reset le timer
        if (Vector3.Distance(transform.position, _path[0]) > _0to1Distance)
        {
            _reComputePathTime = 0;
            return GetNewPath(targetPosition);
        }

        //TODO OPTI nerf le refresh en fonction du nombre de phys agent
        _reComputePathTime += Time.deltaTime;
        if (_reComputePathTime > 1 / _reComputePathPerSecond)
        {
            return GetNewPath(targetPosition);
        }
        return _path;
    }

    private void MoveRigidbody(Vector3[] path)
    {
        if(Vector3.Distance(transform.position, path[1]) < .2f) return;

        Vector3 direction = (path[1] - transform.position).normalized;
        transform.right = Vector3.Lerp(transform.right
                                        , new Vector3(direction.x, 0, direction.z)
                                        , Time.deltaTime * _acceleration * _rotationSpeed * Mathf.Clamp01(_slowMultiplier));

        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity
                                            , direction * Mathf.Clamp01(_slowMultiplier) * _enemySpeed.Value
                                            , Time.fixedDeltaTime * _acceleration);
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _fallingVelocity, _rigidbody.velocity.z);
    }

    private Vector3[] GetNewPath(Vector3 targetPosition)
    {
        _reComputePathTime = 0;
        NavMeshPath newNavPath = new NavMeshPath();
        Vector3[] pathArray;
        if (NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, newNavPath))
        {
            // print("Nav Path : " + newNavPath.corners);
            // print("NavPath count : " + newNavPath.corners.Length);
            // print("NavPath value[0] : " + newNavPath.corners[0] + "||| Current position : " + transform.position);
            //? y'a toujour une erreure chelou ??? index out of range ???
            if (newNavPath.corners.Length <= 1) return _path;
            _0to1Distance = Vector3.Distance(newNavPath.corners[0], newNavPath.corners[1]);
            pathArray = newNavPath.corners;
        }
        else
        {
            //? si jamais l'agent sort du nav mesh
            NavMesh.SamplePosition(transform.position, out NavMeshHit hit, Mathf.Infinity, NavMesh.AllAreas);
            _0to1Distance = Vector3.Distance(transform.position, hit.position);
            pathArray = new[] { transform.position, hit.position };
        }

        //? if no nav mesh the out of NavMesh.SamplePosition if Infinity :/
        if (pathArray[1].x > 100000) return new Vector3[0];
        return pathArray;
    }

    public void SlowAgent(float strenght, float duration, bool freezeAgentOnSlow = false)
    {
        if (freezeAgentOnSlow) _rigidbody.velocity = Vector3.zero;
        _slowMultiplier -= strenght;
        StartCoroutine(ResetSpeed(strenght, duration));
    }

    private IEnumerator ResetSpeed(float strenght, float duration)
    {
        yield return new WaitForSeconds(duration);
        _slowMultiplier += strenght;
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    private void DebugPath()
    {
        if (!DEBUG) return;
        for (int i = 0; i < _path.Length - 1; i++)
        {
            Debug.DrawLine(_path[i], _path[i + 1], Color.red);
        }
    }
}
