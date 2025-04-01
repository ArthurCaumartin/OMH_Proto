using System.Collections.Generic;
using UnityEngine;

public class MobTargetFinder : MonoBehaviour
{
    public bool DEBUG = true;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _targetDetectionPerSecond;
    [SerializeField] private FloatReference _targetDetectionRange;
    [SerializeField] private FloatReference _maxFollowDistance;
    [SerializeField] private FloatReference _timeBeforeCanDropAgro;
    [Space]
    [SerializeField] private MobTarget _ifLostTarget;
    [SerializeField] private MobTarget _currentTarget;
    [SerializeField] private List<string> _banTag;
    private float _distanceWithTarget;
    private float _targetDetectionTime;
    private float _canDropAgro = 1000;

    public Transform Target { get => _currentTarget ? _currentTarget.transform : null; }

    private void Start()
    {
        GetComponent<MobLife>().OnDamageTakenEvent.AddListener(OnDamageTaken);
        if (_ifLostTarget) SetNewTarget(_ifLostTarget);
    }

    private void OnDamageTaken(GameObject damageDealer, DamageType damageType)
    {
        MobTarget t = damageDealer.GetComponent<MobTarget>();
        if (!t) return;
        print(damageDealer.name);

        float distWithDealer = Vector3.Distance(transform.position, damageDealer.transform.position);
        if (distWithDealer < _distanceWithTarget)
        {
            print("Set new target : " + t.name);
            _canDropAgro = 0;
            SetNewTarget(t);
        }
    }

    public void Initialize(MobTarget ifLostTarget)
    {
        _ifLostTarget = ifLostTarget;
    }

    private void Update()
    {
        //TODO delay pour le detect
        DetectNearestTarget();

        if (_currentTarget)
            _distanceWithTarget = Vector3.Distance(transform.position, _currentTarget.transform.position);
        else
            _distanceWithTarget = Mathf.Infinity;

        _canDropAgro += Time.deltaTime;
        if (_distanceWithTarget < _maxFollowDistance.Value) _canDropAgro = _timeBeforeCanDropAgro.Value;

        if (!_currentTarget || IsTargetToFar())
        {
            _currentTarget = _ifLostTarget ? _ifLostTarget : null;
            SetNewTarget(_currentTarget);
        }
    }

    private void DetectNearestTarget()
    {
        _targetDetectionTime += Time.deltaTime;
        if (_targetDetectionTime < 1 / _targetDetectionPerSecond) return;
        _targetDetectionTime = 0;

        Collider[] col = Physics.OverlapSphere(transform.position, _targetDetectionRange.Value, _targetLayer);
        if (col.Length == 0) return;

        float minDistance = Mathf.Infinity;
        MobTarget newTarget = null;
        for (int i = 0; i < col.Length; i++)
        {
            MobTarget currentTarget = col[i].GetComponent<MobTarget>();
            if (!currentTarget) continue;
            if (!currentTarget.enable) continue;
            if (currentTarget == _ifLostTarget) continue;

            float currentDistance = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (currentDistance < minDistance)
            {
                newTarget = currentTarget;
                minDistance = currentDistance;
            }
        }
        if (newTarget) SetNewTarget(newTarget);
    }

    private bool IsTargetToFar()
    {
        if (_currentTarget == _ifLostTarget) return false;
        if (_canDropAgro < _timeBeforeCanDropAgro.Value) return false;

        // print(_distanceWithTarget > _maxFollowDistance.Value ? "target to far" : "target in follow range");
        return _distanceWithTarget > _maxFollowDistance.Value;
    }

    private void SetNewTarget(MobTarget toSet)
    {
        if (toSet && _banTag.Contains(toSet.tag)) return;
        //? si on a une target et que la nouvelle prio est plus grande
        if (_currentTarget && toSet.Priority > _currentTarget.Priority)
        {
            _currentTarget = toSet;
            return;
        }
        _currentTarget = toSet;
    }

    public void ClearTarget()
    {
        _currentTarget = null;
    }

    public void OnDrawGizmos()
    {
        if (!DEBUG) return;
        Gizmos.color = new Color(1, 0, 0, .01f);
        Gizmos.DrawSphere(transform.position, _targetDetectionRange.Value);
        Gizmos.color = new Color(0, 0, 1, .01f);
        Gizmos.DrawSphere(transform.position, _maxFollowDistance.Value);
    }
}
