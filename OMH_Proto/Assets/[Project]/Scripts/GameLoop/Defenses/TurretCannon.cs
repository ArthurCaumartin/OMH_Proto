using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCannon : MonoBehaviour
{
    [SerializeField] protected Projectile _projectilePrefab;
    [SerializeField] protected TurretTargetFinder _finder;
    [SerializeField] protected StatContainer _stat;
    protected Transform _currentTarget;
    protected float _shootTime = 0;
    
    [Tooltip("Modify turret attackSpeed")] //TODO pass variable private and add Getter to get data in UI
    [SerializeField, Range(0.1f, 10)] public float _attackSpeedMultiplier = 1;
    
    [Tooltip("Modify turret damages per bullet")]
    [SerializeField, Range(0.1f, 10)] public float _damagesMultiplier = 1;

    public virtual void Shoot()
    {
        Projectile newProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.LookRotation(transform.forward, Vector3.up));
        newProjectile.Initialize(_stat.projectileSpeed.Value, _stat.damage.Value);
    }

    public virtual void Update()
    {
        if (_finder.GetNearsetMob() != null)
        {
            _currentTarget = _finder.GetNearsetMob().transform;
        }
        if (!_currentTarget) return;
        LookAtTarget();
        ComputeShootTime();
    }

    protected virtual void ComputeShootTime()
    {
        _shootTime += Time.deltaTime;
        if (_shootTime > 1 / _stat.attackPerSecond.Value)
        {
            _shootTime = 0;
            Shoot();
        }
    }

    private void LookAtTarget()
    {
        transform.LookAt(_currentTarget);
    }

    public void SetTarget(Transform target)
    {
        _currentTarget = target;
    }
}
