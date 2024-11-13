using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TargetFinder : MonoBehaviour
{
    public bool DEBUG = true;
    [SerializeField] private PhysicsAgent _agent;
    [SerializeField] private FloatReference _maxFollowDistance;
    private MobTarget _currentTarget;
    private MobTarget _ifLostTarget;

    private void Start()
    {
        _agent = GetComponent<PhysicsAgent>();
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
        float distanceWithTarget = Vector3.Distance(transform.position, _currentTarget.transform.position);
        if (distanceWithTarget > _maxFollowDistance.Value)
            return true;
        return false;
    }

    public void SetAgentTarget(MobTarget toSet)
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
        if(!DEBUG) return;
        Gizmos.color = new Color(1, 0, 0, .2f);
        Gizmos.DrawSphere(transform.position, _maxFollowDistance.Value);
    }
}
