using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobTargetFinder : MonoBehaviour
{
    //TODO prendre l'agro quand on prend un degat, sauf si on target le siphon

    //TODO KNOW ISSUE : If agro taken on a object behind a other the mob will be stuck on without the capability to deal damage to it
    public bool DEBUG = true;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _targetDetectionPerSecond;
    [SerializeField] private float _targetDetectionRange;
    [SerializeField] private FloatReference _maxFollowDistance;
    [Space]
    [SerializeField] private MobTarget _ifLostTarget;
    [SerializeField] private MobTarget _currentTarget;
    private float _distanceWithTarget;
    private float _targetDetectionTime;

    //TODO to remove and pass attack activation to State Machine 
    public float TargetDistance { get => _currentTarget ? _distanceWithTarget : Mathf.Infinity; }
    public Transform Target { get => _currentTarget ? _currentTarget.transform : null; }

    private void Start()
    {
        if (_ifLostTarget) SetNewTarget(_ifLostTarget);
    }

    public void Initialize(MobTarget ifLostTarget)
    {
        _ifLostTarget = ifLostTarget;
    }

    private void Update()
    {
        //TODO delay pour le detect
        DetectTarget();

        if (_currentTarget) _distanceWithTarget = Vector3.Distance(transform.position, _currentTarget.transform.position);
        if (!_currentTarget || IsTargetToFar())
        {
            _currentTarget = _ifLostTarget ? _ifLostTarget : null;
            SetNewTarget(_currentTarget);
        }
    }

    private void DetectTarget()
    {
        _targetDetectionTime += Time.deltaTime;
        if (_targetDetectionTime < 1 / _targetDetectionPerSecond) return;
        _targetDetectionTime = 0;

        Collider[] col = Physics.OverlapSphere(transform.position, _targetDetectionRange, _targetLayer);
        if (col.Length == 0) return;

        float minDistance = Mathf.Infinity;
        MobTarget newTarget = null;
        for (int i = 0; i < col.Length; i++)
        {
            MobTarget currentTarget = col[i].GetComponent<MobTarget>();
            if (!currentTarget) continue;
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

        print(_distanceWithTarget > _maxFollowDistance.Value ? "target to far" : "target in follow range");
        return _distanceWithTarget > _maxFollowDistance.Value;
    }

    private void SetNewTarget(MobTarget toSet)
    {
        //? si on a une target et que la nouvelle prio est plus grande
        if (_currentTarget && toSet.Priority > _currentTarget.Priority)
        {
            _currentTarget = toSet;
            return;
        }
        _currentTarget = toSet;
    }

    public void OnDrawGizmos()
    {
        if (!DEBUG) return;
        Gizmos.color = new Color(1, 0, 0, .2f);
        Gizmos.DrawSphere(transform.position, _maxFollowDistance.Value);
    }
}
