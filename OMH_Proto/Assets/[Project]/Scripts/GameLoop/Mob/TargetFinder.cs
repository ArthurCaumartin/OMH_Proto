using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    //TODO prendre l'agro quand on prend un degat, sauf si on target le siphon

    //TODO KNOW ISSUE : If agro taken on a object behind a other the mob will be stuck on without the capability to deal damage to it
    public bool DEBUG = true;
    [SerializeField] private PhysicsAgent _agent;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _targetDetectionPerSecond;
    [SerializeField] private float _targetDetectionRange;
    [SerializeField] private FloatReference _maxFollowDistance;
    [Space]
    [SerializeField] private MobTarget _ifLostTarget;
    [SerializeField] private MobTarget _currentTarget;
    private float _distanceWithTarget;
    private float _targetDetectionTime;

    public float TargetDistance { get => _currentTarget ? _distanceWithTarget : Mathf.Infinity; }
    public GameObject Target { get => _currentTarget ? _currentTarget.gameObject : null; }

    private void Start()
    {
        _agent = GetComponentInParent<PhysicsAgent>();

        if (_ifLostTarget) SetAgentTarget(_ifLostTarget);
    }

    public void Initialize(MobTarget ifLostTarget)
    {
        _ifLostTarget = ifLostTarget;
    }

    private void Update()
    {
        DetectTarget();

        if (_currentTarget) _distanceWithTarget = Vector3.Distance(transform.position, _currentTarget.transform.position);
        if (!_currentTarget || IsTargetToFar())
        {
            // print("Target lost !");
            _currentTarget = _ifLostTarget ? _ifLostTarget : null;
            // print("Target set to : " + (_currentTarget ? _currentTarget.name : "NULL"));
            SetAgentTarget(_currentTarget);
        }
    }

    private void DetectTarget()
    {
        _targetDetectionTime += Time.deltaTime;
        if (_targetDetectionTime > 1 / _targetDetectionPerSecond)
        {
            _targetDetectionTime = 0;
            Collider[] col = Physics.OverlapSphere(transform.position, _targetDetectionRange, _targetLayer);
            for (int i = 0; i < col.Length; i++)
            {
                MobTarget t = col[i].GetComponent<MobTarget>();
                if(t == _ifLostTarget) continue;
                if (t) SetAgentTarget(t);
            }
        }
    }

    private bool IsTargetToFar()
    {
        if(_currentTarget == _ifLostTarget) return false;
        return _distanceWithTarget > _maxFollowDistance.Value;
    }

    private void SetAgentTarget(MobTarget toSet)
    {
        if (!toSet)
        {
            _agent.SetTarget(null);
            return;
        }

        

        if (_currentTarget)
        {
            _agent.SetTarget(_currentTarget.transform);
        }

        if (!_currentTarget || toSet.Priority > _currentTarget.Priority)
        {
            // print("Set / " + toSet.name + " / to new target !");
            _currentTarget = toSet;
            _agent.SetTarget(_currentTarget.transform);
            return;
        }
    }

    public void OnDrawGizmos()
    {
        if (!DEBUG) return;
        Gizmos.color = new Color(1, 0, 0, .2f);
        Gizmos.DrawSphere(transform.position, _maxFollowDistance.Value);
    }
}
