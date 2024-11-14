using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TargetFinder : MonoBehaviour
{
    public bool DEBUG = true;
    [SerializeField] private PhysicsAgent _agent;
    [SerializeField] private FloatReference _maxFollowDistance;
    [SerializeField] private MobTarget _ifLostTarget;
    [SerializeField] private MobTarget _currentTarget;
    private float _distanceWithTarget;

    public float TargetDistance { get => _currentTarget ? _distanceWithTarget : Mathf.Infinity; }
    public GameObject Target { get => _currentTarget ? _currentTarget.gameObject : null; }

    private void Start()
    {
        _agent = GetComponentInParent<PhysicsAgent>();

        if(_ifLostTarget) SetAgentTarget(_ifLostTarget);
    }

    public void Initialize(MobTarget ifLostTarget)
    {
        _ifLostTarget = ifLostTarget;
    }

    private void Update()
    {
        if (!_currentTarget) return;

        if (IsTargetToFar())
        {
            // print("Target et so NULL");
            _currentTarget = _ifLostTarget ? _ifLostTarget : null;
            SetAgentTarget(_currentTarget);
        }
    }

    private bool IsTargetToFar()
    {
        _distanceWithTarget = Vector3.Distance(transform.position, _currentTarget.transform.position);
        if (_distanceWithTarget > _maxFollowDistance.Value)
            return true;
        return false;
    }

    private void SetAgentTarget(MobTarget toSet)
    {
        if (!toSet)
        {
            // print("YAYA");
            _agent.SetTarget(null);
            return;
        }

        if (!_currentTarget || toSet.Priority > _currentTarget.Priority)
        {
            // print("Set / " + toSet.name + " / to new target !");
            _currentTarget = toSet;
            _agent.SetTarget(_currentTarget.transform);
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == transform) return;
        MobTarget t = other.GetComponent<MobTarget>();
        if (!t) return;
        SetAgentTarget(t);
    }

    public void OnDrawGizmos()
    {
        if (!DEBUG) return;
        Gizmos.color = new Color(1, 0, 0, .2f);
        Gizmos.DrawSphere(transform.position, _maxFollowDistance.Value);
    }
}
