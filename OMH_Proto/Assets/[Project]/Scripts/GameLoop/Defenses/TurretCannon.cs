using UnityEngine;

public class TurretCannon : MonoBehaviour
{
    [SerializeField] protected Transform _projectileSpawnPivot;
    [SerializeField] protected Projectile _projectilePrefab;
    [SerializeField] protected TurretTargetFinder _finder;
    [SerializeField] protected StatContainer _stat;
    protected Transform _currentTarget;
    protected float _shootTime = 0;

    [Tooltip("Modify turret attackSpeed")]
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
        if (!_currentTarget)
        {
            if(_finder.GetNearsetMob() != null) _currentTarget = _finder.GetNearsetMob()?.transform;
        }
        if(!_currentTarget) return;

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
        // transform.LookAt(_currentTarget);
        // transform.forward = new Vector3(transform.forward.x, 0, transform.forward.y).normalized;

        Vector3 lootAt = (_currentTarget.position - transform.position).normalized;
        lootAt.y = 0;
        transform.forward = lootAt;
    }

    public void SetTarget(Transform target)
    {
        _currentTarget = target;
    }
}
